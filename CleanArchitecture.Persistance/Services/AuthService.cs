using AutoMapper;
using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Email;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(UserManager<User> userManager, IMapper mapper, IMailService mailService, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mailService = mailService;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginCommandResponse> CreateNewTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null
                || user.RefreshToken != request.RefreshToken
                || user.RefreshTokenExpires is null
                || user.RefreshTokenExpires <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token geçersiz veya süresi dolmuş.");
            }

            return await _jwtProvider.CreateTokenAsync(user);
        }

        public async Task<LoginCommandResponse> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserNameOrEmail)
               ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Kullanıcı adı/e-posta veya şifre hatalı.");

            return await _jwtProvider.CreateTokenAsync(user);

        }

        public async Task RegisterAsync(RegisterCommand request)
        {
            User user = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var failures = result.Errors
                    .Select(e => new ValidationFailure(e.Code, e.Description));

                throw new FluentValidation.ValidationException(failures);
            }

            await _mailService.SendAsync(
                        user.Email!,
                        "Kayıt başarılı",
                        $"<h3>Merhaba {user.FullName}</h3><p>Hesabın başarıyla oluşturuldu.</p>");

        }
    }
}
