# Demo app

This is the final demo application from Pluralsight course:
[Building a Web App with ASP.NET Core, MVC, Entity Framework Core, Bootstrap, and Angular](https://app.pluralsight.com/library/courses/aspnetcore-mvc-efcore-bootstrap-angular-web/table-of-contents)

## Prerequisites

npm (angular, angular-cli, gulp, gulp-cli)
```
npm install -g @angular/cli
npm install -g gulp
npm install -g gulp-cli
ng --version
```

## Angular

Angular magic to link to .NET Core 2.0 project

#### Basic commands

```
ng new client-app --minimal --dry-run
ng new client-app --minimal
ng build
ng serve
ng build
ng build --watch
```

#### Actual magic

```
mkdir ClientApp
move client-app/src/* ClientApp/
copy client-app/.angular-cli.json .
copy client-app/package.json .
copy client-app/tsconfig.json . /Y
rd /Q /S client-app
```
Unload project and load it back again

In `tsconfig.json` change `outDir` value to `./wwwroot/clientapp/out-tsc` and add following lines:
```
"include": [
  "./ClientApp",
  "./wwwroot/ts"
]
```

In `.angular-cli.json` change `app.root` value to `ClientApp` and `app.outDir` value to `./wwwroot/ClientApp/dist`
In `.angular-cli.json` delete `app.index`

Now issue
```
npm install
ng build
ng build --watch
```

Refer to `Views/App/Shop.cshtml`