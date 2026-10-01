# Clean Architecture Web API

.NET 9 ile Clean Architecture prensiplerine göre geliştirilmiş bir Web API eğitim projesi.

## Kullanılan Teknolojiler

- .NET 9 / ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity
- MediatR (CQRS)
- FluentValidation
- AutoMapper
- JWT Authentication + Refresh Token
- MailKit (e-posta gönderimi)
- Swagger

## Özellikler

- Katmanlı mimari: Domain, Application, Infrastructure, Persistence, Presentation, WebApi
- CQRS ile command/query ayrımı
- Pipeline behavior ile otomatik validation
- Global exception middleware
- Generic repository ve Unit of Work
- Sayfalama (pagination) ve arama
- Kullanıcı kaydı, giriş ve refresh token ile token yenileme
- Kayıt sonrası e-posta gönderimi
- Rol yönetimi ve veritabanı tabanlı rol kontrolü (custom authorization filter)

## Kurulum

1. `appsettings.json` içindeki `ConnectionStrings:SqlServer` değerini kendi SQL Server bağlantınıza göre düzenleyin.
2. JWT anahtarını tanımlayın (en az 32 karakter)
3. 3. E-posta gönderimi için (isteğe bağlı) `Email` ayarlarını doldurun; şifreyi user-secrets ile verin.
4. Veritabanını oluşturun
5. 5. Projeyi çalıştırın ve Swagger arayüzünü açın.

## Bilinçli Olarak Bırakılan Eksikler

Bu proje bir eğitim çalışmasıdır. Aşağıdaki konular, sonraki projelerde ele alınmak üzere bilinçli olarak basit tutulmuştur:

- İş mantığı handler yerine service katmanında
- Entity'ler anemic (rich domain model yok)
- Hata yönetimi exception tabanlı, `ProblemDetails` kullanılmıyor
- Refresh token kullanıcı tablosunda düz metin olarak, kullanıcı başına tek token
- Birim testleri sınırlı
