# ASP.NET Core Backend for the T-Shirt Shop Web App

[![Build status](https://ci.appveyor.com/api/projects/status/p3iet9wrmg8jxlmx?svg=true)](https://ci.appveyor.com/project/jincod/aspnet5demoapp)

## Usage

```bash
npm install
dotnet restore
dotnet run -p src/Tss/Tss.csproj
npm start
```

## Deploy to Heroku

### Manual

Using custom buildpack [dotnetcore-buildpack](https://github.com/jincod/dotnetcore-buildpack)

```bash
heroku buildpacks:set https://github.com/jincod/dotnetcore-buildpack
heroku buildpacks:add --index 1 heroku/nodejs
```

[Using Multiple Buildpacks for an App](https://devcenter.heroku.com/articles/using-multiple-buildpacks-for-an-app)

### Heroku Deploy button

Click the button below to set up this sample app on Heroku:

[![Deploy](https://www.herokucdn.com/deploy/button.svg)](https://heroku.com/deploy?template=https://github.com/jincod/AspNet5DemoApp)

## Blog posts

- [Deploying ASP.NET Core on heroku](https://jincod.tumblr.com/post/152290263970/deploying-aspnet-core-on-heroku)
- [Integrate ASP.NET 5 and Webpack with Hot Module Replacement plugin](http://jincod.tumblr.com/post/135043543538/integrate-aspnet-5-and-webpack-with-hot-module)

## Databases Setup

This app uses MySQL database in local development environment and ClearBD when deployed to Heroku.

In local environment, the connection string should be specified in the `appsettings.Development.json` file with following format:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;uid=root;pwd=password;database=tshirtshop"
  }
}

```

To know how to extract connection string from the `appsettings.Development.json`, please refer to the below articles:

- [How To Connect MySQL With ASP.NET Core
](https://www.c-sharpcorner.com/article/how-to-connect-mysql-with-asp-net-core/)
- [How to reverse engineer MySql Database EF Core
](https://garywoodfine.com/how-to-reverse-engineer-mysql-database-ef-core/)

To know how to get Heroku environment variables, please refer to:

- [How to get Heroku environment variables](https://github.com/jincod/dotnetcore-buildpack/issues/33)

To migrate the above local database to Heroku's ClearDB, you use the following command:

```bash
heroku addons:create cleardb:ignite -a tssnetcore --name database-name --fork=mysql://user:password@myhostname.com/database-name
```

# T-Shirt Shop, aka. TSS, Project 

## Description

This app is an online shop for customers to order and buy T-shirt. 

## Branching Model

![git-model](https://nvie.com/img/git-model@2x.png)

This repository follows the practices in [A successful Git branching model](https://nvie.com/posts/a-successful-git-branching-model/) article by [@nvie](https://twitter.com/nvie). Thus, we have the following additional branches:

- Master branch: `HEAD` always reflects a production-ready state.

- Dev branch: `HEAD` always reflects a state with the latest delivered development changes for the next release.

- Feature branches: May branch off from:
`dev`. Must merge back into: `dev`.

- Release branches: May branch off from:
`dev`. Must merge back into:
`dev` and `master`.

- Hotfix branches: May branch off from:
`master`. Must merge back into:
`dev` and `master`.

I recommend you to use Git Flow which follows the above branching model. Git Flow is available in both Sourcetree and GitKraken. Personally, I would recommend you to use [GitKraken](https://www.gitkraken.com/) since they support more Git operations such as Drop a Commit.

Additionally, both Sourcetree and GitKraken support [Git Large File Storage (LFS)](https://git-lfs.github.com/) to store large files in somewhere else, but you still be able to check them out from GitHub just like a normal file!

## CI/CD Flow

### Build flow for v1.0

`.travis.yml > Dockerfile > package.json > server.js`

`.travis.yml` first installs Minikube, Kubectl and Kubernetes. 

Then, it installs Docker, build and run the image. During the run, the Dockerfile reads `package.json` file, installs npm packages, and runs npm start, which eventually run `server.js` file.

Server.js file creates an Express server and responds to HTTP requests with the text "Hello world".

The port to communicate with the server is by default `process.env.PORT` or `8080`.

### CI/CD flow for v1.0

CI is executed by Travis CI. If the build in Travis CI passes or fails, the user will be notified on GitHub or by email. 

The passed build on master branch will be deployed to Heroku production server and exposed at `tshirtshopminh.herokuapp.com`. 

To test a specific branch, you could change the branch name at deploy.app in `.travis.yml`. For example, if you want to deploy the branch `features/initial-setup`, the config at `deploy.app` would be     

```yaml
deploy:
  provider: heroku
  api_key:
    secure: $HEROKU_API_KEY
  app:
    master: tshirtshopminh
    features/initial-setup: tsstest
```

The passed build on this branch will be deployed to Heroku test server and exposed at `tsstest.herokuapp.com`.

## Environment variables

The `$HEROKU_API_KEY` is set in the Settings options of Travis CI.

In Travis CI, to define variables in Repository Settings, make sure you’re logged in, navigate to the repository in question, choose “Settings” from the cog menu, and click on “Add new variable” in the “Environment Variables” section.

![settings-env-vars](https://docs.travis-ci.com/images/settings-env-vars.png)

## Deploying to Vercel

Create a Vercel project for this repository and deploy it with Vercel's Container Runtime (Beta). The root-level `Dockerfile.vercel` builds and runs the ASP.NET Core API. The database must be reachable from Vercel.

This project targets .NET Core 3.1, which is out of support; upgrade the runtime before using this deployment for production.

Set these backend environment variables in the Vercel project:

- `ENV=production`
- `AUTHORIZATION_TOKEN_SECRET`
- `JAWSDB_CONNECTION` (ADO.NET MySQL connection string, e.g. `Server=<host>;Port=<port>;Database=<database>;User ID=<user>;Password=<password>;SslMode=Required`; do not use a `mysql://` URL)
- `CLOUDINARY_CLOUD_NAME`
- `CLOUDINARY_API_KEY`
- `CLOUDINARY_API_SECRET`
- `FRONTEND_URL` (optional; defaults to `https://tssnextjs.vercel.app` and is used to build password-reset links)

The application applies EF Core migrations during startup. Configure any additional integration variables required by the enabled email, payment, or image-upload features in `src/Core/Models/AppSettings.cs`.

Deploy `tssnextjs` as a separate Vercel project and set its `API_URL` to the backend deployment URL, including the `/api/` path and trailing slash (for example, `https://<backend-domain>/api/`).

## Testing

Use the following command `dotnet test --logger "console;verbosity=detailed" --filter "Priority=<priority>"` to run tests of specified priority. 

Highest priority is 1, which covers critical business cases.