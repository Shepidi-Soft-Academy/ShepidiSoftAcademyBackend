<img width="180" height="180" alt="ShepidiSoft Logo" src="https://github.com/user-attachments/assets/a7fe3623-7a4b-4553-8cb8-71850ce411d4" />

# ShepidiSoft Academy Management — Backend

> **Kurumsal eğitim ve organizasyon süreçlerini tek merkezde yöneten, .NET 8 tabanlı Clean Architecture Web API**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://learn.microsoft.com/en-us/aspnet/core/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-EF%20Core%208-336791?style=flat-square&logo=postgresql)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat-square&logo=docker)](https://www.docker.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=flat-square&logo=swagger)](https://swagger.io/)
[![License](https://img.shields.io/badge/License-Private-red?style=flat-square)](#lisans)

---

## 📋 İçindekiler

- [Genel Bakış](#genel-bakış)
- [Özellikler](#özellikler)
- [Teknoloji Yığını](#teknoloji-yığını)
- [Mimari](#mimari)
- [Proje Yapısı](#proje-yapısı)
- [Domain Modeli](#domain-modeli)
- [API Endpoint'leri](#api-endpointleri)
- [Teknik Desenler](#teknik-desenler)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
- [Docker ile Çalıştırma](#docker-ile-çalıştırma)
- [Yapılandırma](#yapılandırma)
- [Güvenlik](#güvenlik)
- [Lisans](#lisans)

---

## Genel Bakış

**ShepidiSoft Academy Management Backend**, eğitim kurumlarının tüm operasyonel süreçlerini dijital ortama taşımayı hedefleyen, kurumsal düzeyde bir REST API altyapısıdır.

Platform; kurs yönetimi, öğrenci/eğitmen takibi, ödev akışları, organizasyon yönetimi, kariyer başvuruları, duyurular ve topluluk hizmetleri gibi birçok farklı modülü **tek bir teknik omurgada** birleştirir. Sistem, **Clean Architecture** prensipleri ve **CQRS** deseni üzerine inşa edilmiş olup ölçeklenebilirlik, test edilebilirlik ve sürdürülebilirlik önceliklendirilmiştir.

---

## Özellikler

| Özellik | Açıklama |
|---|---|
| 🏗️ **Clean Architecture** | `API → Application → Domain → Infrastructure` katman ayrımı |
| ⚡ **CQRS + MediatR** | Command ve Query'lerin birbirinden tam ayrımı |
| ✅ **Validation Pipeline** | `FluentValidation` ile merkezi ve pipeline tabanlı doğrulama |
| 🔐 **JWT Authentication** | Bearer token tabanlı kimlik doğrulama ve rol bazlı yetkilendirme |
| 🗃️ **EF Core + PostgreSQL** | Repository pattern ve Unit of Work üzerinden veri erişimi |
| 📬 **Outbox + Background Jobs** | Asenkron e-posta gönderimi için güvenilir outbox pattern |
| 📁 **Dosya Yönetimi** | Sunucu taraflı dosya yükleme/silme servisi (PDF, DOCX, görsel) |
| 🤖 **Gemini AI Entegrasyonu** | Google Gemini API üzerinden yapay zeka desteği |
| 📊 **Audit Logging** | EF Core interceptor ile otomatik `CreatedBy/UpdatedBy` takibi |
| 📧 **E-posta Şablonları** | MailKit + HTML template tabanlı e-posta bildirimleri |
| 🐳 **Docker Desteği** | Multi-stage Dockerfile ve Docker Compose yapılandırması |
| 📖 **Swagger/OpenAPI** | JWT destekli tam API dokümantasyonu |

---

## Teknoloji Yığını

### Core Framework
| Teknoloji | Versiyon | Kullanım Amacı |
|---|---|---|
| **.NET** | 8.0 | Uygulama çalışma zamanı |
| **ASP.NET Core Web API** | 8.0 | HTTP katmanı ve endpoint yönetimi |
| **C#** | 12 | Birincil programlama dili |

### Veri Katmanı
| Teknoloji | Versiyon | Kullanım Amacı |
|---|---|---|
| **Entity Framework Core** | 8.0.23 | ORM ve migration yönetimi |
| **Npgsql (PostgreSQL)** | 8.0.11 | Üretim veritabanı sürücüsü |
| **ASP.NET Core Identity** | 8.0.23 | Kullanıcı yönetimi ve rol sistemi |

### Uygulama Katmanı
| Teknoloji | Versiyon | Kullanım Amacı |
|---|---|---|
| **MediatR** | 13.1.0 | CQRS mesajlaşma mediator |
| **FluentValidation** | 12.1.1 | Request doğrulama pipeline'ı |
| **AutoMapper** | 16.1.1 | Entity ↔ DTO dönüşümleri |

### Güvenlik & İletişim
| Teknoloji | Versiyon | Kullanım Amacı |
|---|---|---|
| **Microsoft.AspNetCore.Authentication.JwtBearer** | 8.0.23 | JWT Bearer token doğrulama |
| **MailKit** | 4.16.0 | SMTP e-posta gönderimi |

### DevOps & Dokümantasyon
| Teknoloji | Versiyon | Kullanım Amacı |
|---|---|---|
| **Swashbuckle (Swagger)** | 6.6.2 | OpenAPI dokümantasyonu |
| **Docker** | — | Konteynerizasyon |
| **Docker Compose** | — | Geliştirme ortamı yönetimi |

---

## Mimari

ShepidiSoft, **Clean Architecture** prensiplerini esas alan, net sorumluluk sınırlarına sahip dört katmanlı bir yapı üzerine inşa edilmiştir:

```
┌──────────────────────────────────────────────────────┐
│                    API Katmanı                        │
│   Controllers · Middleware · Filters · OptionsSetup   │
└───────────────────────┬──────────────────────────────┘
                        │  MediatR Commands & Queries
┌───────────────────────▼──────────────────────────────┐
│                Application Katmanı                    │
│    Features (CQRS) · Behaviours · Contracts · DTOs    │
└───────────────────────┬──────────────────────────────┘
                        │  Domain Entities & Interfaces
┌───────────────────────▼──────────────────────────────┐
│                  Domain Katmanı                       │
│         Entities · Enums · Base Classes               │
└───────────────────────┬──────────────────────────────┘
                        │  Interface Implementations
┌───────────────────────▼──────────────────────────────┐
│               Infrastructure Katmanı                  │
│  Persistence · Identity · Notification · BackgroundJobs│
└──────────────────────────────────────────────────────┘
```

### Katman Sorumlulukları

| Katman | Sorumluluk |
|---|---|
| **API** | HTTP endpoint'lerini dışarıya açar; istek/yanıt döngüsünü, CORS, Auth middleware'ini ve Swagger'ı yönetir |
| **Application** | Use-case'leri CQRS deseninde (Command/Query) organize eder; validation, mapping ve servis sözleşmelerini barındırır |
| **Domain** | Çekirdek iş varlıklarını (Entity), enum'ları ve temel soyutlamaları tanımlar; dış bağımlılıktan tamamen bağımsızdır |
| **Infrastructure** | Veritabanı, kimlik doğrulama, e-posta bildirimi ve arka plan işleri gibi dış bağımlılıkları uygular |

---

## Proje Yapısı

```
ShepidiSoftAcademyManagement/
│
├── .github/                          # GitHub Actions (CI/CD)
│
└── ShepidiSoftAcademy/
    ├── docker-compose.yml            # Docker servis tanımları
    ├── docker-compose.override.yml   # Geliştirme ortamı overrides
    ├── ShepidiSoft.Storage/          # Dosya yükleme/silme modülü
    │   ├── FileStorageService.cs     # wwwroot tabanlı dosya servisi
    │   └── DependencyInjection.cs
    │
    └── src/
        ├── api/
        │   └── ShepidiSoft.API/
        │       ├── Abstraction/
        │       │   └── BaseApiController.cs   # JWT auth + MediatR temel controller
        │       ├── Controllers/               # 24 adet endpoint controller
        │       ├── Extensions/               # Identity extension metodları
        │       ├── Filters/
        │       │   └── FluentValidationFilter.cs  # Global validation filter
        │       ├── OptionsSetup/             # JWT options yapılandırması
        │       ├── Templates/               # E-posta HTML şablonları
        │       ├── Program.cs               # Uygulama başlangıç noktası
        │       └── Dockerfile               # Multi-stage container build
        │
        ├── core/
        │   ├── ShepidiSoft.Application/
        │   │   ├── Behaviours/
        │   │   │   └── ValidationBehaviour.cs  # MediatR pipeline validation
        │   │   ├── Contracts/
        │   │   │   ├── Common/              # ICurrentUserService vb.
        │   │   │   ├── Identity/            # Auth servis sözleşmeleri
        │   │   │   ├── Notification/        # IEmailService sözleşmesi
        │   │   │   ├── Persistence/         # 25 adet Repository interface
        │   │   │   └── IFileStorageService.cs
        │   │   ├── Features/               # 22 domain feature (CQRS)
        │   │   │   ├── Activities/
        │   │   │   ├── Announcements/
        │   │   │   ├── Assignments/
        │   │   │   ├── AssignmentSubmission/
        │   │   │   ├── Auths/
        │   │   │   ├── CareerApplications/
        │   │   │   ├── CollaborationApplications/
        │   │   │   ├── CommunityServices/
        │   │   │   ├── ContactMessages/
        │   │   │   ├── Courses/
        │   │   │   ├── DocumentTopics/
        │   │   │   ├── Documents/
        │   │   │   ├── GetAnalytics/
        │   │   │   ├── Instructors/
        │   │   │   ├── Meetings/
        │   │   │   ├── Newss/
        │   │   │   ├── OrganizationMembers/
        │   │   │   ├── OrganizationPositions/
        │   │   │   ├── Outbox/
        │   │   │   ├── StudentRequests/
        │   │   │   ├── Students/
        │   │   │   └── Users/
        │   │   ├── Enums/
        │   │   └── ServiceResult.cs        # Generic sonuç sarmalayıcısı
        │   │
        │   └── ShepidiSoft.Domain/
        │       └── Entities/
        │           ├── Common/
        │           │   ├── BaseEntity.cs      # Generic Id tabanlı temel entity
        │           │   └── IAuditEntity.cs    # Created/Updated audit interface
        │           ├── Enums/                 # Domain enum'ları
        │           └── Organizations/         # Organization entity cluster
        │
        └── infrastructure/
            ├── ShepidiSoft.Persistence/
            │   ├── Context/
            │   │   └── AppDbContext.cs       # IdentityDbContext türevi, 20+ DbSet
            │   ├── Interceptors/
            │   │   └── AuditDbContextInterceptor.cs  # Otomatik audit takibi
            │   ├── Migrations/               # EF Core migration geçmişi
            │   ├── Seedings/                 # Geliştirme ortamı seed verileri
            │   ├── GenericRepository.cs      # Temel CRUD repository
            │   ├── UnitOfWork.cs
            │   └── [Entity]Repository/       # 25 adet özel repository
            │
            ├── ShepidiSoft.Identity/
            │   ├── Auths/
            │   │   └── Jwt/                  # JWT token üretimi
            │   ├── Models/                   # ApplicationUser genişletmesi
            │   ├── Services/                 # Kimlik doğrulama servisleri
            │   └── Roles.cs                  # Rol sabitleri
            │
            ├── ShepidiSoft.Notification/
            │   ├── Mail/                     # MailKit e-posta servisi
            │   ├── Options/                  # EmailSettings yapılandırması
            │   └── Templates/               # HTML e-posta şablonları
            │
            └── ShepidiSoft.BackgroundJobs/
                └── Outbox/
                    └── OutboxProcessorJob.cs  # 30s aralıklı e-posta işleyici
```

---

## Domain Modeli

Projede aşağıdaki 20+ domain entity bulunmakta olup hepsi `AppDbContext` üzerinden yönetilmektedir:

### Eğitim Modülleri

| Entity | Açıklama |
|---|---|
| `Course` | Kurs bilgileri, eğitmen ataması, kurs üyelikleri |
| `CourseMembership` | Öğrenci–Kurs ilişki tablosu |
| `Instructor` | Eğitmen profili ve kurs atamaları |
| `Student` | Öğrenci bilgileri ve kayıt durumu |
| `Assignment` | Kurs bazlı ödev tanımları |
| `AssignmentSubmission` | Öğrenci ödev teslimleri |
| `Activity` | Kurs aktiviteleri ve etkinlikler |
| `Meeting` | Çevrimiçi/yüz yüze toplantı kayıtları |
| `Announcement` | Duyuru yönetimi |

### Organizasyon Modülleri

| Entity | Açıklama |
|---|---|
| `Organization` | Kurum/organizasyon tanımı |
| `OrganizationMember` | Organizasyon üye kaydı |
| `OrganizationPosition` | Organizasyon içi pozisyon tanımı |
| `OrganizationMemberPosition` | Üye–Pozisyon çoktan çoğa ilişkisi |

### İletişim & Başvuru Modülleri

| Entity | Açıklama |
|---|---|
| `ContactMessage` | İletişim formu mesajları |
| `CareerApplication` | Kariyer başvuruları (durum takipli) |
| `CollaborationApplication` | İş birliği başvuruları |
| `StudentRequest` | Öğrenci talep/istek yönetimi |

### İçerik & Diğer

| Entity | Açıklama |
|---|---|
| `Document` | Belge yönetimi (PDF, DOCX vb.) |
| `DocumentTopic` | Belge konuları/kategorileri |
| `Project` | Proje portföy yönetimi |
| `ProjectImage` | Proje görselleri |
| `News` | Haber/blog yönetimi |
| `CommunityService` | Topluluk hizmetleri |
| `OutboxMessage` | Asenkron mesaj kuyruğu |

### Durum Enum'ları

```csharp
ApplicationStatus       // Pending · Approved · Rejected
CollaborationApplicationStatus
DocumentStatus          // Draft · Published · Archived
StudentRequestStatus    // Pending · InProgress · Resolved · Closed
```

---

## API Endpoint'leri

Tüm endpoint'ler `BaseApiController`'dan türemekte ve varsayılan olarak **JWT Bearer Authentication** gerektirmektedir.

| Controller | Prefix | Açıklama |
|---|---|---|
| `AuthsController` | `/api/auths` | Kayıt, giriş, şifre sıfırlama |
| `CoursesController` | `/api/courses` | CRUD + üye/ödev yönetimi |
| `StudentsController` | `/api/students` | Öğrenci CRUD |
| `InstructorsController` | `/api/instructors` | Eğitmen CRUD |
| `AssignmentsController` | `/api/assignments` | Ödev CRUD |
| `AssignmentSubmissionsController` | `/api/assignmentsubmissions` | Teslimat yönetimi |
| `ActivitiesController` | `/api/activities` | Aktivite yönetimi |
| `MeetingsController` | `/api/meetings` | Toplantı yönetimi |
| `AnnouncementsController` | `/api/announcements` | Duyuru yönetimi |
| `OrganizationsController` | `/api/organizations` | Organizasyon CRUD |
| `OrganizationMembersController` | `/api/organizationmembers` | Üye yönetimi |
| `OrganizationPositionsController` | `/api/organizationpositions` | Pozisyon yönetimi |
| `ContactMessagesController` | `/api/contactmessages` | İletişim form yönetimi |
| `CareerApplicationController` | `/api/careerapplication` | Kariyer başvuruları |
| `CollaborationApplicationsController` | `/api/collaborationapplications` | İş birliği başvuruları |
| `StudentRequestsController` | `/api/studentrequests` | Öğrenci talep yönetimi |
| `DocumentsController` | `/api/documents` | Belge yükleme/yönetim |
| `DocumentTopicsController` | `/api/documenttopics` | Belge konu yönetimi |
| `ProjectsController` | `/api/projects` | Proje yönetimi |
| `ProjectImagesController` | `/api/projectimages` | Proje görselleri |
| `NewsController` | `/api/news` | Haber/blog yönetimi |
| `CommunityServicesController` | `/api/communityservices` | Topluluk hizmetleri |
| `AnalyticsController` | `/api/analytics` | Analitik veriler |
| `UsersController` | `/api/users` | Kullanıcı yönetimi |

---

## Teknik Desenler

### 1. CQRS + MediatR

Her use-case, `Command` (yazma) veya `Query` (okuma) olarak ayrı sınıflarda tanımlanır:

```
Features/
  Courses/
    Commands/
      CreateCourse/   → CreateCourseCommand + Handler + Validator
      UpdateCourse/   → UpdateCourseCommand + Handler + Validator
      DeleteCourse/   → DeleteCourseCommand + Handler
    Queries/
      GetCourses/     → GetCoursesQuery + Handler
    CourseMappingProfile.cs
```

### 2. Validation Pipeline

```csharp
// MediatR pipeline behavior — tüm command'lar otomatik doğrulanır
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
```

FluentValidation kuralları handler'a ulaşmadan tetiklenir; başarısız validasyon doğrudan `400 Bad Request` döner.

### 3. ServiceResult Pattern

Tüm handler'lar `ServiceResult<T>` döner; bu sayede HTTP durum kodları tutarlı biçimde yönetilir:

```csharp
ServiceResult<T>.Success(data)               // 200 OK
ServiceResult<T>.SuccessAsCreated(data, url) // 201 Created
ServiceResult<T>.Fail("Hata mesajı")         // 400 Bad Request
ServiceResult<T>.Fail("Bulunamadı", HttpStatusCode.NotFound) // 404
```

### 4. Outbox Pattern (Asenkron E-posta)

```
İstek gelir → Handler OutboxMessage kaydeder → 200 OK döner
      ↓
OutboxProcessorJob (BackgroundService, 30sn aralıklı)
      ↓
Pending mesajları alır → MailKit ile e-posta gönderir → IsSent = true
```

Bu yaklaşım; e-posta gönderimi başarısız olsa bile ana işlem etkilenmez ve yeniden deneme imkânı sunar.

### 5. Audit Interceptor

`SaveChangesInterceptor` türevi `AuditDbContextInterceptor`, `IAuditEntity` arayüzünü uygulayan tüm entity'lerde:
- **Ekleme** sırasında → `Created`, `CreatedBy` otomatik doldurulur
- **Güncelleme** sırasında → `Updated`, `UpdatedBy` otomatik doldurulur

Mevcut kullanıcı bilgisi, `ICurrentUserService` aracılığıyla `IHttpContextAccessor`'dan alınır.

### 6. Repository + Unit of Work

```csharp
IGenericRepository<TEntity>    // GetAll, GetById, Add, Update, Delete
IUnitOfWork                    // SaveChangesAsync
```

Her aggregate root için `IGenericRepository<T>` türeyen özel repository arayüzleri bulunur.

---

## Kurulum ve Çalıştırma

### Ön Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [PostgreSQL](https://www.postgresql.org/) (yerel kurulum veya Docker)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) *(opsiyonel)*

### 1. Repoyu Klonlayın

```bash
git clone https://github.com/your-username/ShepidiSoftAcademyManagement.git
cd ShepidiSoftAcademyManagement
```

### 2. Bağlantı Dizesini Yapılandırın

`ShepidiSoftAcademy/src/api/ShepidiSoft.API/appsettings.Development.json` dosyasını düzenleyin:

```json
{
  "ConnectionStrings": {
    "Npgsql": "Host=localhost;Port=5432;Database=shepidisoftacademy;Username=postgres;Password=yourpassword"
  },
  "JWT": {
    "Issuer": "ShepidiSoft",
    "Audience": "shepidisoft.net",
    "SecretKey": "your-very-secure-secret-key-min-32-chars",
    "ExpiryMinutes": 60
  },
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "ShepidiSoft",
    "SenderEmail": "your-email@gmail.com",
    "Password": "your-app-password"
  }
}
```

> ⚠️ **Hassas bilgileri** (connection string, secret key, e-posta şifresi) `appsettings.json`'a **asla commit etmeyin**. `User Secrets` veya environment variable kullanın.

### 3. Veritabanı Migration'larını Uygulayın

```bash
cd ShepidiSoftAcademy/src/api/ShepidiSoft.API
dotnet ef database update --project ../../infrastructure/ShepidiSoft.Persistence
```

### 4. Uygulamayı Başlatın

```bash
dotnet run --project ShepidiSoftAcademy/src/api/ShepidiSoft.API
```

Uygulama başladığında:
- **Swagger UI:** `https://localhost:7149/swagger`
- **HTTP API:** `http://localhost:5000`

> Development modunda uygulama başlangıcında veritabanı seed verisi otomatik yüklenir.

### 5. User Secrets (Önerilen)

```bash
cd ShepidiSoftAcademy/src/api/ShepidiSoft.API
dotnet user-secrets set "ConnectionStrings:Npgsql" "Host=localhost;..."
dotnet user-secrets set "JWT:SecretKey" "your-secret-key"
```

---

## Docker ile Çalıştırma

```bash
cd ShepidiSoftAcademy
docker-compose up --build
```

| Port | Servis |
|---|---|
| `8080` | HTTP |
| `8081` | HTTPS |

**Docker Compose yapılandırması:**
- `docker-compose.yml` → Üretim servis tanımı
- `docker-compose.override.yml` → Geliştirme ortamı ayarları (port, volume, environment)

---

## Yapılandırma

### appsettings.json Yapısı

```json
{
  "ConnectionStrings": {
    "Npgsql": "<PostgreSQL bağlantı dizesi>"
  },
  "JWT": {
    "Issuer": "ShepidiSoft",
    "Audience": "shepidisoft.net",
    "SecretKey": "<min. 32 karakter gizli anahtar>",
    "ExpiryMinutes": 60
  },
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "ShepidiSoft",
    "SenderEmail": "<gönderici e-posta>",
    "Password": "<uygulama şifresi>"
  },
  "GeminiSettings": {
    "ApiKey": "<Google Gemini API Key>",
    "ApiUrl": "https://generativelanguage.googleapis.com"
  },
  "AppSettings": {
    "BaseUrl": "https://your-domain.com"
  }
}
```

---

## Güvenlik

### Kimlik Doğrulama Akışı

```
POST /api/auths/login
    → Kullanıcı adı/şifre doğrulama (ASP.NET Core Identity)
    → JWT Bearer Token üretimi
    → Token yanıtta döner

Sonraki istekler:
    Authorization: Bearer <token>
    → JwtBearerOptionsSetup doğrular
    → BaseApiController [Authorize] attribute'u korur
```

### Yetkilendirme Modeli

- **Varsayılan:** Tüm endpoint'ler `[Authorize(AuthenticationSchemes = "Bearer")]` ile korunur
- **Public erişim:** Gerekli endpoint'lerde `[AllowAnonymous]` ile kontrollü açılır (örn. iletişim formu, kariyer başvurusu)
- **Rol bazlı:** `Roles.cs` sabitlerinden tanımlanan roller üzerinden `[Authorize(Roles = "...")]` uygulanabilir

### Dosya Yükleme Güvenliği

```csharp
// İzin verilen formatlar
string[] AllowedExtensions = [".pdf", ".docx", ".doc", ".xlsx", ".png", ".jpg"];
long MaxBytes = 10 * 1024 * 1024; // 10 MB limit
```

---

## Lisans

Bu repository özel (private) kullanım için geliştirilmiştir.  
Kullanım, dağıtım ve katkı koşulları için lütfen repository sahibiyle iletişime geçin.

---

<div align="center">

**ShepidiSoft Academy Management Backend**  
`.NET 8` · `Clean Architecture` · `CQRS` · `PostgreSQL` · `Docker`

</div>