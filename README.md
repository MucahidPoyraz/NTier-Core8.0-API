# Proje: ASP.NET Core ile Çok Katmanlı Mimari Web API Geliştirme ve JWT İle Kimlik Doğrulama

## Proje Açıklaması

Bu projede, **ASP.NET Core** kullanarak çok katmanlı mimariye sahip bir **Web API** geliştirdim. Proje, bir blog uygulamasının arka planını oluşturmayı amaçlamakta ve **Kategori (Category)** ile **Blog** olmak üzere iki ana varlık (_entity_) üzerinde tam kapsamlı **CRUD (Create, Read, Update, Delete)** işlemlerini gerçekleştirmektedir. Ayrıca, **JWT (JSON Web Token)** ile kimlik doğrulama mekanizması ekleyerek API güvenliğini sağladım.

## Kullanılan Teknolojiler ve Araçlar

- **ASP.NET Core Web API**: RESTful API geliştirmek için temel framework.
- **Entity Framework Core**: Veritabanı işlemleri ve ORM (_Object-Relational Mapping_) için.
- **MS SQL Server**: Veritabanı yönetim sistemi.
- **AutoMapper**: Entity ve DTO (_Data Transfer Object_) nesneleri arasında otomatik dönüşüm sağlamak için.
- **FluentValidation**: Veri doğrulama işlemlerini kolay ve etkili bir şekilde gerçekleştirmek için.
- **JWT (JSON Web Token)**: API'de kimlik doğrulama ve yetkilendirme işlemleri için.
- **Repository ve Unit of Work Tasarım Desenleri**: Veri erişim katmanında daha modüler ve test edilebilir bir yapı için.
- **Generic Repository ve Generic Manager Sınıfları**: Tekrarlayan kodları azaltmak ve genel CRUD işlemlerini yönetmek için.
- **Özelleştirilmiş Response Yapısı**: API yanıtlarını standartlaştırmak ve tutarlı hale getirmek için.
- **API Versioning (Sürümleme)**: Farklı API sürümlerini yönetmek ve gelecekteki genişletilebilirliği sağlamak için.
- **Katmanlı Mimari Prensipleri**: Uygulama, farklı sorumluluklara sahip katmanlara ayrılmıştır (**DAL**, **BLL**, **API**, **DTO**).

## Proje Özellikleri

### Katmanlı Mimari

Uygulama, **Data Access Layer (DAL)**, **Business Logic Layer (BLL)**, **API**, ve **DTO** katmanlarına ayrılarak geliştirilmiştir. Bu sayede her katman kendi sorumluluklarını taşır ve modüler bir yapı sağlanır.

### RESTful API Geliştirme

- **Tam Kapsamlı CRUD İşlemleri**: API üzerinden **Kategori** ve **Blog** verileri için oluşturma, okuma, güncelleme ve silme işlemleri gerçekleştirildi.
- **API Sürümleme**: Route yapılarına `api/v1/` şeklinde sürüm bilgisi eklenerek gelecekteki genişletilebilirlik sağlandı.
- **Swagger Entegrasyonu**: API dokümantasyonu ve testleri için Swagger kullanıldı.
- **Esnek Endpoint Yapısı**: Hem filtreleme hem de ilişkili verileri dahil etme imkanı sunan esnek endpoint'ler oluşturuldu.

### JWT ile Kimlik Doğrulama ve Yetkilendirme

- **JWT Kullanımı**: API'ye erişimi kontrol etmek ve güvenliği sağlamak için JWT tabanlı kimlik doğrulama mekanizması entegre edildi.
- **Token Oluşturma ve Doğrulama**: Kullanıcı girişinde token oluşturma, her istek ile gelen token'ı doğrulama ve yetkilendirme işlemleri gerçekleştirildi.
- **Rol Bazlı Erişim Kontrolü**: Farklı kullanıcı rolleri için (örneğin, **Admin**, **User**) farklı yetkilendirme politikaları uygulandı.

### Generic Repository ve Manager Kullanımı

- Tek bir generic repository ve manager sınıfı ile farklı entity'ler üzerinde işlem yapabilme.
- Kod tekrarının önlenmesi ve daha temiz bir kod yapısı.

### FluentValidation ile Veri Doğrulama

- Kullanıcıdan gelen verilerin doğruluğunu ve tutarlılığını sağlamak için **FluentValidation** kullanıldı.
- Örneğin, **BlogValidation** sınıfı ile blog başlığı, içerik ve diğer alanların doğrulaması yapıldı.

### AutoMapper Entegrasyonu

- Entity ve DTO nesneleri arasında otomatik dönüşüm sağlandı.
- **AutoMapper Profilleri** oluşturularak mapping işlemleri kolaylaştırıldı.

### Özelleştirilmiş Response Yapısı

- **IResponse** ve **ITResponse<T>** arayüzleri ile API yanıtları standartlaştırıldı.
- Başarılı ve başarısız işlemler için tutarlı yanıtlar ve hata mesajları sağlandı.

### Dependency Injection ve SOLID Prensipleri

- Tüm servisler ve repository'ler **dependency injection** ile enjekte edildi.
- **SOLID** prensipleri gözetilerek esnek ve sürdürülebilir bir kod yapısı oluşturuldu.

### Entity Framework Core ile Veritabanı İşlemleri

- **Code First** yaklaşımı ile veritabanı tabloları oluşturuldu.
- **DbContext** sınıfı ve **Entity Configuration** sınıfları ile veritabanı yapılandırması yapıldı.

### Hata Yönetimi ve Loglama

- **Try-catch** blokları ve özel exception sınıfları ile hata yönetimi.
- Hataların loglanması ve kullanıcıya anlamlı mesajlar iletilmesi.

## Kazandığım Deneyimler ve Yetkinlikler

- **ASP.NET Core ve Web API Geliştirme**: RESTful API standartlarına uygun olarak API geliştirme konusunda deneyim kazandım.
- **JWT ile Kimlik Doğrulama**: API güvenliğini sağlamak için JWT teknolojisini entegre ettim ve kimlik doğrulama süreçlerini yönettim.
- **Katmanlı Mimari ve Tasarım Desenleri**: Uygulamayı katmanlara ayırarak, her katmanın sorumluluklarını net bir şekilde belirledim ve ölçeklenebilir bir mimari oluşturdum.
- **Entity Framework Core**: Veritabanı işlemlerini yönetmek için EF Core'u etkin bir şekilde kullandım.
- **AutoMapper ve FluentValidation Entegrasyonu**: Kod içinde verimli veri dönüşümleri ve veri doğrulama süreçleri oluşturdum.
- **Generic Yapılar ile Esneklik**: Generic repository ve manager sınıfları sayesinde tekrar eden kodları azaltarak daha temiz ve bakımı kolay bir kod tabanı elde ettim.
- **API Sürümleme ve Swagger Kullanımı**: Farklı API sürümlerini yönetmeyi ve dokümantasyonunu sağlamayı öğrenerek, uygulamanın gelecekteki ihtiyaçlara uyum sağlamasını kolaylaştırdım.
- **Hata Yönetimi ve Kullanıcıya Geri Bildirim**: Kullanıcı deneyimini iyileştirmek için tutarlı ve anlamlı hata mesajları ve yanıtlar oluşturdum.
- **SOLID Prensipleri ve Temiz Kod Uygulamaları**: Yazılım geliştirme süreçlerimde SOLID prensiplerini uygulayarak kaliteyi ve sürdürülebilirliği artırdım.

## Sonuç

Bu proje, yazılım geliştirme becerilerimi pekiştirmemi ve modern web uygulamalarının arka planını oluşturan teknolojilerde derinlemesine bilgi sahibi olmamı sağladı. Özellikle **API geliştirme**, **JWT ile kimlik doğrulama** ve **güvenlik** konularında deneyim kazandım. Proje boyunca karşılaştığım zorlukları çözmek ve farklı teknolojileri entegre etmek, problem çözme yeteneklerimi geliştirdi ve profesyonel anlamda büyümeme katkı sağladı.

---

**Not:** Bu proje, kompleks sistemlerin nasıl tasarlanacağı ve geliştirileceği konusunda bana değerli içgörüler kazandırdı ve edindiğim deneyimleri gelecekteki projelerimde uygulamayı hedefliyorum.
