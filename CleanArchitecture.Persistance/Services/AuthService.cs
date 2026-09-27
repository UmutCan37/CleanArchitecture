using AutoMapper;
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

        public AuthService(UserManager<User> userManager, IMapper mapper, IMailService mailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mailService = mailService;
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
