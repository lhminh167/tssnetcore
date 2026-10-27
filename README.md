# T-Shirt Shop API

**A REST API for a full-stack e-commerce storefront, built with ASP.NET Core, C#, and MySQL.**

This backend powers the [T-Shirt Shop frontend](https://github.com/lhminh167/tssnextjs), handling the catalog and customer journeys from browsing and authentication through checkout, order management, and email notifications. The codebase is organized into Core, Infrastructure, and Web layers, with separate unit, integration, and functional test projects.

**Live API:** [tssnetcore.vercel.app](https://tssnetcore.vercel.app/) <br>
**Interactive API docs:** [Swagger UI](https://tssnetcore.vercel.app/swagger) <br>
**Frontend:** [tssnextjs.vercel.app](https://tssnextjs.vercel.app/)

## What it does

- **Catalog APIs:** homepage product collections, filtering, product details, recommendations, categories, departments, and attributes.
- **Customer accounts:** registration, JWT login, customer profiles, password changes, and password-reset flows with email notifications.
- **Shopping cart:** cart retrieval and updates, item quantities, save-for-later, move-to-cart, and product recommendations.
- **Checkout and orders:** checkout form data, Stripe-backed payment processing, order creation and history, and stock/shipping status workflows.
- **Reviews and media:** product reviews and review management, plus Cloudinary image-upload support.
- **Store operations:** protected endpoints for managing products, customers, attributes, and order fulfilment.
- **API contract and errors:** Swagger/OpenAPI documentation, request validation, consistent response envelopes, and centralized exception handling.

## Technology

| Area | Tools |
| --- | --- |
| API | ASP.NET Core Web API, C# |
| Persistence | Entity Framework Core, MySQL, Pomelo |
| Authentication | JWT bearer tokens |
| Payments and email | Stripe, SendGrid |
| Product media | Cloudinary |
| API documentation | Swagger / OpenAPI |
| Tests | xUnit, ASP.NET Core test host, EF Core InMemory |
| Deployment | Docker, Vercel Container Runtime |

> **Runtime note:** This repository currently targets .NET Core 3.1, which is out of support. Upgrade to a supported .NET release before using this stack for a new production system.

## API capabilities

Routes are grouped under `/api`:

| Resource | Example operations |
| --- | --- |
| `Product` | `GET /Homepage`, `GET /Filter`, product details, recommendations, reviews, create/update/delete |
| `Catalog`, `Category`, `Department` | Browse catalog metadata, categories, and departments |
| `Customer` | Register, `POST /Login`, profile management, password change and recovery |
| `ShoppingCart` | Read/update cart, recommendations, save for later, move items back to cart |
| `Order` | Checkout data, payment, order creation/history, stock and shipping updates |
| `Review` | Create, view, update, and delete reviews |
| `Media` | Request an image-upload token, update or delete media |
| `Attribute`, `AttributeValue` | Read and manage product/customer attributes |

Most endpoints require a JWT bearer token. Login and password recovery are anonymous; administrative and order-fulfilment actions are authorization protected. Consult Swagger for request schemas and the full route list.

## Getting started

### Requirements

- .NET Core 3.1 SDK (the target framework in `src/Tss.csproj`)
- MySQL database accessible from the development machine
- Integration credentials for the services used by the features you want to run (for example, SendGrid, Stripe, or Cloudinary)

### Restore and run

```bash
dotnet restore Tss.sln
dotnet build Tss.sln
dotnet run --project src/Tss.csproj
```

The API uses the `Development` environment through the project launch profile and listens on the configured local ASP.NET Core ports. Swagger is available at `/swagger`.

For a custom local URL, set `ASPNETCORE_ENVIRONMENT=Development` and pass a URL:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project src/Tss.csproj --urls http://127.0.0.1:5009
```

Provide local settings in `src/appsettings.Development.json` under an `AppSettings` object. Keep real credentials in local-only configuration or a secret manager; do not commit them. The project `.gitignore` excludes the development settings file.

### Configuration

In production (`ENV=production`), `AppSettings` reads these environment variables:

| Variable | Purpose |
| --- | --- |
| `JAWSDB_CONNECTION` | ADO.NET MySQL connection string |
| `AUTHORIZATION_TOKEN_SECRET` | Signing secret for JWT authentication |
| `SENDGRID_API_KEY` | SendGrid API key for outgoing email |
| `CUSTOMER_SERVICE_EMAIL`, `CUSTOMER_SERVICE_NAME` | Sender identity for customer emails |
| `SUPPLIER_EMAIL`, `SUPPLIER_NAME` | Supplier notification identity |
| `ADMIN_EMAIL`, `ADMIN_NAME` | Administrative contact |
| `STRIPE_SECRET_KEY`, `STRIPE_PUBLIC_KEY` | Stripe integration keys |
| `CLOUDINARY_CLOUD_NAME`, `CLOUDINARY_API_KEY`, `CLOUDINARY_API_SECRET` | Cloudinary integration credentials |
| `CLOUDINARY_CLOUD_FOLDER` | Optional Cloudinary upload folder |
| `FRONTEND_URL` | Frontend base URL for password-reset links; defaults to `https://tssnextjs.vercel.app` |

Set `JAWSDB_CONNECTION` to a MySQL ADO.NET connection string, not a `mysql://` URL. Store its user name and password in the production environment or a secret manager.

Use test/sandbox credentials during development. Never put live API keys, database passwords, JWT secrets, or other credentials in source control.

## Tests

The solution has three test projects:

```bash
dotnet test tests/UnitTests/UnitTests.csproj
dotnet test tests/IntegrationTests/IntegrationTests.csproj
dotnet test tests/FunctionalTests/FunctionalTests.csproj
```

To run tests by priority where the test has a `Priority` trait:

```bash
dotnet test Tss.sln --filter "Priority=1"
```

Functional tests include API/controller scenarios and use the ASP.NET Core test host and EF Core InMemory provider. Check each test fixture for any additional configuration it requires.

## Project structure

The solution separates business rules from external integrations and HTTP concerns:

```text
src/
  Core/            Domain entities, use cases, interfaces, and business rules
  Infrastructure/  Database access and integrations
  Web/             ASP.NET Core controllers and HTTP configuration
  Properties/      Local launch profiles
  Log/             Runtime log output
tests/
  UnitTests/       Focused unit tests
  IntegrationTests/Integration-level tests
  FunctionalTests/ API, domain, and end-to-end behavior tests
databases/         SQL database snapshots and maintenance scripts
Dockerfile.vercel  Vercel container build and startup
vercel.json        Vercel service routing
```

### `src/Core/` — domain and application rules

- `Entities/` models customers, products, shopping carts, reviews, and orders. Aggregate-specific behavior lives in `CustomerAggregate/`, `ProductAggregate/`, and `OrderAggregate/`; customer and order email templates are grouped alongside their aggregates.
- `Services/` coordinates application use cases such as customer, product, cart, and order workflows.
- `Specifications/` describes reusable repository queries and filtering rules.
- `Interfaces/` defines contracts for repositories and external services so Core is not tied to infrastructure implementations.
- `Models/` contains API request/response and application-settings models.
- `Guards/` and `Exceptions/` hold business validation and domain-specific errors.
- `Helpers/`, `Extensions/`, and `Constants/` provide reusable domain utilities.

### `src/Infrastructure/` — persistence and integrations

- `Data/` contains `TssContext`, Entity Framework mappings, repositories, query evaluation, and `Migrations/`.
- `Helpers/` implements integrations and technical services such as authentication/JWT, SendGrid email, Stripe payments, media, and request-related helpers.

### `src/Web/` — HTTP and application host

- `Controllers/` exposes the REST resources, including customers, products, orders, shopping carts, reviews, categories, departments, attributes, and media.
- `Configs/` wires dependency injection, authentication, routing, persistence, Swagger, logging, and application services.
- `ActionFilters/` contains request/model validation filters.
- `Middlewares/` contains HTTP pipeline middleware, including centralized error handling.

### `tests/` — verification at multiple levels

- `UnitTests/` isolates individual business rules and utilities.
- `IntegrationTests/` verifies behavior across collaborating components.
- `FunctionalTests/Core/` tests domain entities, helpers, and models.
- `FunctionalTests/Web/ControllerTests/` exercises controller behavior by feature; `Fixtures/` and `Urls/` hold reusable test setup and endpoint helpers.

### `databases/` and deployment files

- `databases/` contains versioned SQL snapshots and targeted database scripts for development/reference.
- `Dockerfile.vercel` builds and runs the API container; `vercel.json` routes project traffic to the container service.
- `docker-compose.yml`, `Dockerfile`, and pipeline configuration files support other local/container/CI workflows.

## Deployment

The API is deployed independently from the frontend using Vercel's Container Runtime. Configure production environment variables in the Vercel project, ensure the MySQL database permits connections from the deployment environment, and deploy from the repository root.

The application applies EF Core migrations during startup and serializes concurrent migration attempts with a MySQL advisory lock. Product names use a UTF-8 `varchar(255)` column and descriptions use `varchar(1000)`, supporting Vietnamese and other Unicode text. Review migrations and take database backups before deploying schema changes. Point the frontend's `API_URL` at the deployed API base URL, including `/api/` and a trailing slash.

## Related project

The matching storefront is maintained in [tssnextjs](https://github.com/lhminh167/tssnextjs). The frontend provides the customer-facing Next.js experience; this repository supplies its API and commerce workflows.
