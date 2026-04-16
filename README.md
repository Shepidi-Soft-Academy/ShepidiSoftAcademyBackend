# ShepidiSoft Backend

<img width="500" height="500" alt="ShepidiSoft Logo" src="https://github.com/user-attachments/assets/a7fe3623-7a4b-4553-8cb8-71850ce411d4" />

ShepidiSoft Backend, egitim ve organizasyon sureclerini tek merkezde yonetmeyi hedefleyen, `.NET 8` tabanli katmanli bir Web API altyapisidir.

Platform; kurumlarin kurs, egitmen, ogrenci, aktivite ve gorev (assignment) akislarini dijital ortama tasirken; iletisim ve kariyer basvurulari gibi operasyonel surecleri de ayni teknik omurgada birlestirir.

Bu yapinin temel amaci:

- egitim operasyonlarini olceklenebilir bir servis mimarisiyle yonetmek,
- farkli modullerin birbiriyle tutarli calismasini saglamak,
- degisime acik, bakimi kolay ve uretime uygun bir backend standardi sunmaktir.

## Ozellikler

- Clean Architecture benzeri katmanli yapi (`api`, `core`, `infrastructure`)
- CQRS yaklasimi (`MediatR`) ile command/query ayrimi
- `FluentValidation` ile pipeline tabanli request dogrulama
- JWT tabanli kimlik dogrulama ve role bazli yetkilendirme
- `Entity Framework Core` + `SQL Server` persistence katmani
- Email gonderimi ve outbox tabanli background isleme
- Swagger/OpenAPI dokumantasyonu

## Teknoloji Yigini

- `.NET 8`
- `ASP.NET Core Web API`
- `Entity Framework Core 8`
- `MediatR`
- `FluentValidation`
- `AutoMapper`
- `ASP.NET Core Identity`
- `MailKit`
- `Swashbuckle (Swagger)`

## Proje Yapisi

```text
ShepidiSoft/
  src/
    api/
      ShepidiSoft.API                # HTTP endpoint'ler, middleware ve API konfig
    core/
      ShepidiSoft.Application        # Use case'ler, CQRS, validasyon, kontratlar
      ShepidiSoft.Domain             # Domain entity ve temel modelleme
    infrastructure/
      ShepidiSoft.Persistence        # EF Core, repository'ler, DbContext, migration'lar
      ShepidiSoft.Identity           # Auth/JWT/Identity servisleri
      ShepidiSoft.Notification       # E-posta servisleri ve template'ler
      ShepidiSoft.BackgroundJobs     # Outbox processor background service
```

## Domain Kapsami (Yuksek Seviye)

Projede asagidaki alanlar icin controller ve use-case yapisi bulunur:

- Auth
- Courses
- Instructors
- Students
- Activities
- Assignments / Assignment Submissions
- Organizations / Organization Members / Organization Positions
- Contact Messages
- Career Applications
- Documents / Document Topics
- Projects / Project Images
- Analytics

## Teknik Mimari Yaklasim

ShepidiSoft, sorumluluklari net ayiran bir katmanli tasarim uzerine kuruludur:

- `API` katmani, HTTP endpoint'leri ve istek/yanit davranisini yonetir.
- `Application` katmani, use-case odakli is kurallarini `CQRS` (command/query) deseninde organize eder.
- `Domain` katmani, cekirdek varliklari ve alan modelini barindirir.
- `Infrastructure` katmani, veritabani, kimlik, bildirim ve background islemler gibi dis bagimliliklari ustlenir.

Bu ayrim sayesinde sistem:

- moduler bicimde genisletilebilir,
- test edilebilirlik ve surdurulebilirlik kazanir,
- domain odakli gelisime daha uygun hale gelir.

## Uygulanan Teknik Prensipler

- `CQRS + MediatR`: Islemler command ve query olarak ayrilarak okunabilirlik ve ayrik sorumluluk saglanir.
- `Validation Pipeline`: `FluentValidation` ile talepler merkezi ve standart bir sekilde dogrulanir.
- `JWT Tabanli Guvenlik`: Kimlik dogrulama ve rol bazli yetkilendirme ile kontrollu erisim modeli uygulanir.
- `EF Core Persistence`: Veri erisimi `Entity Framework Core` ve repository yapilariyla yonetilir.
- `Outbox + Background Processing`: Asenkron bildirim akislari (or. email) guvenli sekilde arka planda islenir.

## Kurumsal Deger Onermesi

ShepidiSoft Backend, yalnizca endpoint sunan bir API degil; kurumsal egitim ve insan kaynaklari odakli surecleri teknik olarak standardize eden bir altyapidir.

Bu yaklasimla proje:

- operasyonel karmasikligi azaltmayi,
- veri akislarini merkezilestirmeyi,
- zamanla buyuyen urun ihtiyaclarina mimari olarak uyum saglamayi
hedefler.

## Guvenlik ve Yetkilendirme

- Varsayilan endpoint davranisi JWT bearer authentication uzerinden korunur.
- `BaseApiController` seviyesinde merkezi authorization kurgusu uygulanmistir.
- Gereken senaryolarda `AllowAnonymous` ile public erisim kontrollu sekilde acilir.

## Arka Plan Isleri (Outbox)

- `ShepidiSoft.BackgroundJobs` icindeki `OutboxProcessorJob` hosted service olarak calisir.
- Belirli araliklarla bekleyen outbox mesajlarini alir ve email gonderimini tetikler.
- Basarili/hatali islem sonucunu outbox kaydina yazar.

## Gelecek Vizyonu

ShepidiSoft Backend, moduler yapisi sayesinde yeni domain'lerin (or. raporlama, gelismis analiz, yeni entegrasyonlar) eklenmesine uygun sekilde tasarlanmistir. Mimari tercihlerin ana hedefi, urunun sadece bugunku ihtiyaclarini degil; orta ve uzun vadeli buyume gereksinimlerini de karsilayabilmesidir.

## Lisans

Bu repoda acik bir lisans dosyasi bulunmuyor.  
Lutfen kullanim ve dagitim kosullari icin repo sahibine danisin.