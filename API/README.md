## API

[![API](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/api-dotnet.yml/badge.svg)](https://github.com/louisaxel-ambroise/eonix-test/actions/workflows/api-dotnet.yml)

Ce projet est une API web qui contient des endpoints CRUD qui permettent de gérer des entités `Personne`.
Les données sont conservées dans une base de données SQLite, et la persistence est assurée par EntityFramework Core.

### Choix techniques

La logique de récupération/stockage des données est gérée directement dans le controlleur. 
Cette approche a été choisie car cette API est déstinée à être intégrée dans une application basée sur des micro-services. Chaque service devant être minimal et indépendant, la simplicité du modèle et des endpoints ne nécessitait pas une architecture plus complexe *(AMA)*.

Le controlleur accepte un `EonixContext` en paramètre (injecté par DI), et permet de manipuler les entités de la base de données grâce à la librairie EF Core.

L'API respecte l'architecture REST, et propose les endpoints suivants:
- GET: liste les entités/chercheune entité par ID
- POST: ajoute une entité dans la DB
- PUT: mise à jour d'une entité
- DELETE: suppression d'une entité

### Évolution

Si l'API devait venir à évoluer et/ou si le modèle venait à changer (plus d'entités, ajout de relations, etc), il serait à envisager d'utiliser une architecture plus complexe, probablement en utilisant [Mediatr](https://github.com/jbogard/MediatR) et [AutoMapper](https://github.com/AutoMapper/AutoMapper).

L'ajout d'une méthode d'authentification est également requise avant de déployer l'application.

### HowTo

Vérifier que la connectionstring pour la base de données est configurée dans le fichier `appSettings.json` (ou `appSettings.[environnement].json`). Le nom de la connectionString est `PersonnesDatabase`:
```
"ConnectionStrings": {
  "PersonnesDatabase": "Data Source=eonix.dev.db"
}
```

L'API peut ensuite être lancée avec les commandes:

```
> dotnet build Eonix.Api\Eonix.Api.csproj
> dotnet run --project Eonix.Api\Eonix.Api.csproj --urls=http://localhost:5002
```

Les migrations EF Core sont appliquées automatiquement à la base de données au démarrage de l'API. Il est également possible de les appliquer manuellement avec la commande:

```
> dotnet ef database update --project Eonix.Api\Eonix.Api.csproj
```


La documentation OpenAPI (Swagger) de l'application se trouve à l'URL [http://localhost:5002/swagger/index.html](http://localhost:5002/swagger/index.html)

Le projet contient également des tests unitaires:
```
> dotnet test
```