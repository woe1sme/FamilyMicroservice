# Семейная Бухгалтерия

## Описание проекта

**Семейная Бухгалтерия** - это микросервисное приложение для управления семейным бюджетом. Оно позволяет отслеживать семейные финансы, управлять расходами, доходами, членами семьи и их ролями в бюджете.

Проект разработан на платформе .NET с использованием принципов чистой архитектуры и микросервисной архитектуры.

## Архитектура

Проект разделен на несколько микросервисов, каждый из которых отвечает за определенную область функциональности. Данный микросервис предоставляет следующие основные возможности:

- **Управление семьями**: создание, редактирование и получение информации о семьях.
- **Управление членами семьи**: добавление, обновление и удаление членов семьи.
- **Роли членов семьи**: назначение ролей (глава семьи, взрослый, ребенок) членам семьи.

### Структура проекта

Архитектура микросервиса разделена на слои:

- **API**: наружный слой, предоставляющий конечные точки для взаимодействия с клиентами и другими сервисами.
- **Бизнес-логика**: реализует правила и поведение приложения.
- **Домен**: содержит бизнес-логики и основные сущности.
- **Инфраструктура**: отвечает за взаимодействие с внешними системами, такими как базы данных и другие сервисы.

### Технологии

- **.NET 8**
- **ASP.NET Core**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker и Docker Compose**

## Установка и запуск

### Предварительные требования

- Установленные **Docker** и **Docker Compose**.
- Свободные порты **5000** и **5432** на вашем хосте.

### Шаги установки

1. **Клонируйте репозиторий проекта:**

   ```bash
   git clone https://github.com/SerafimLazuko/FamilyMicroservice.git
   cd FamilyMicroservice

2. **Настройте переменные окружения:**

Создайте файл .env в корневой директории проекта со следующим содержимым:
```
POSTGRES_USER=postgres
POSTGRES_PASSWORD=YourPassword
POSTGRES_DB=FamilyDb
```

3. **Запустите приложение с помощью Docker Compose:**
```
docker-compose up --build
```
Это запустит:
- Контейнер с приложением API на порту 5000.
- Контейнер с базой данных PostgreSQL на порту 5432

4. **Проверьте работоспособность API:**
Откройте браузер и перейдите по адресу http://localhost:5000/swagger, чтобы увидеть Swagger UI с документацией API.

## Документация API
Полная документация API доступна через **Swagger UI** по адресу http://localhost:5000/swagger.

API предоставляет следующие основные конечные точки:
- /api/families: управление семьями.
- /api/families/{familyId}/members: управление членами семьи внутри семьи.

## Тестирование
Для тестирования API вы можете использовать следующие инструменты:
- HTTP-файл: в проекте имеется файл Family.API.http с примерами запросов, который можно открыть в VS Code или Visual Studio.
- Postman: импортируйте коллекцию запросов из папки PostmanCollection и используйте их для тестирования.
- Swagger UI: позволяет отправлять запросы прямо из интерфейса Swagger.

## Заполнение базы данных тестовыми данными
**Использование SQL-скриптов**
Вы можете использовать SQL-скрипты для заполнения таблиц тестовыми данными. Для этого подключитесь к базе данных PostgreSQL с помощью клиента, такого как HeidiSQL, pgAdmin или DBeaver.

Пример SQL-запросов:

```sql
-- Добавление семьи
INSERT INTO "Families" ("Id", "Name")
VALUES
  ('{новый GUID}', 'Doe Family');

-- Добавление членов семьи
INSERT INTO "FamilyMembers" ("Id", "Name", "Role", "FamilyId", "UserId")
VALUES
  ('{новый GUID}', 'John Doe', 'HeadOfFamily', '{FamilyId}', '{UserId}'),
  ('{новый GUID}', 'Jane Doe', 'Adult', '{FamilyId}', '{UserId}');
```
**Использование Seed Data в приложении**

Вы можете настроить инициализацию данных при запуске приложения.

Добавьте класс SeedData:

```csharp
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new FamilyDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<FamilyDbContext>>());

        // Проверка наличия данных
        if (context.Families.Any())
        {
            return;
        }

        // Создание тестовых данных
        var family = new Family
        {
            Id = Guid.NewGuid(),
            Name = "Doe Family",
            // Дополнительные поля
        };

        context.Families.Add(family);

        var familyHead = new FamilyMember
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Role = Role.HeadOfFamily,
            FamilyId = family.Id,
            UserId = Guid.NewGuid()
        };

        context.FamilyMembers.Add(familyHead);

        context.SaveChanges();
    }
}
```
Вызовите инициализацию в Program.cs:

```csharp
var builder = WebApplication.CreateBuilder(args);
// ...

var app = builder.Build();

// Инициализация данных
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

app.Run();
```

## Вклад в проект

Мы приветствуем вклад в развитие проекта! Если вы хотите принять участие:

- Сделайте форк репозитория.

- Создайте ветку с новой функцией или исправлением:

```bash
git checkout -b feature/YourFeature
```
- Внесите изменения и сделайте коммит:
```
bash
git commit -am 'Добавил новую функцию'
```
- Отправьте изменения в свой форк:
```
bash
git push origin feature/YourFeature
```
- Создайте Pull Request.

**Пожалуйста, убедитесь, что ваш код соответствует стилю проекта и проходит все тесты.**

## Лицензия
Этот проект распространяется под лицензией **MIT**. Подробности смотрите в файле [LICENSE](https://github.com/SerafimLazuko/FamilyMicroservice/blob/master/LICENSE).
