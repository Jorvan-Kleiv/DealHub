# DealHub 🔥

> Plateforme communautaire de partage et découverte de bons plans.

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet)
![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-4-06B6D4?style=flat-square&logo=tailwindcss)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver)

---

## Table des matières

- [Stack technique](#stack-technique)
- [Prérequis](#prérequis)
- [Installation](#installation)
- [Configuration](#configuration)
- [Base de données](#base-de-données)
- [Lancer le projet](#lancer-le-projet)
- [Structure du projet](#structure-du-projet)
- [Workflow de développement](#workflow-de-développement)
- [Migrations](#migrations)
- [Problèmes fréquents](#problèmes-fréquents)
- [Contribution](#contribution)

---

## Stack technique

| Couche          | Technologie                  |
|-----------------|------------------------------|
| Backend         | ASP.NET Core 10 MVC          |
| ORM             | Entity Framework Core 10     |
| Base de données | SQL Server / LocalDB         |
| CSS             | Tailwind CSS v4              |
| Auth            | ASP.NET Core Identity        |
| Icons           | Iconify — Solar              |

---

## Prérequis

Avant de commencer, assurez-vous d'avoir installé :

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js 20+](https://nodejs.org)
- [SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads) ou SQL Server LocalDB (inclus avec Visual Studio)
- [Git](https://git-scm.com)

Vérifier les versions installées :
```bash
dotnet --version   # doit afficher 10.x.x
node --version     # doit afficher 20.x.x ou supérieur
npm --version      # doit afficher 10.x.x ou supérieur
```

---

## Installation

### 1. Cloner le dépôt
```bash
git clone https://github.com/votre-user/dealhub.git
cd dealhub
```

### 2. Installer les dépendances Node
```bash
npm install
```

### 3. Restaurer les packages .NET
```bash
dotnet restore
```

---

## Configuration

### 1. Créer le fichier de configuration local

Le fichier `appsettings.Development.json` n'est **pas versionné** (données sensibles).  
Copiez le template fourni :
```bash
# Linux / macOS
cp appsettings.Example.json appsettings.Development.json

# Windows (PowerShell)
Copy-Item appsettings.Example.json appsettings.Development.json
```

### 2. Renseigner vos valeurs

Ouvrez `appsettings.Development.json` et remplacez les valeurs :
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DealHub;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

> **SQL Server distant ?** Remplacez par :
> ```
> Server=mon-serveur;Database=DealHub;User Id=mon-user;Password=mon-mdp;TrustServerCertificate=True;
> ```

> **LocalDB non disponible ?** Installez-le via :
> ```bash
> # Inclus avec Visual Studio, ou via :
> winget install Microsoft.SQLServer.2022.Express
> ```

---

## Base de données

### Appliquer les migrations existantes
```bash
dotnet ef database update
```

Cela crée la base de données et applique toutes les migrations versionnées.

### Vérifier que la base est bien créée
```bash
dotnet ef migrations list
```

Toutes les migrations doivent afficher `(Applied)`.

---

## Lancer le projet

### Workflow recommandé — deux terminaux en parallèle

**Terminal 1 — Tailwind CSS en mode watch**
```bash
npm run dev
```

**Terminal 2 — ASP.NET Core**
```bash
dotnet watch run
```

L'application est disponible sur :
- **HTTPS** → `https://localhost:5001`
- **HTTP** → `http://localhost:5000`

> Les ports peuvent varier selon votre `Properties/launchSettings.json`.

---

## Structure du projet
```
DealHub/
│
├── Areas/
│   └── Admin/                  # Zone d'administration
│       ├── Controllers/
│       └── Views/
│
├── Controllers/                # Controllers principaux
├── Models/                     # Modèles EF Core + DTOs
├── Views/                      # Vues Razor
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _DealStatusBadge.cshtml
│   └── Deals/
│
├── wwwroot/
│   ├── css/
│   │   └── output.css          # ⚠️ Généré par Tailwind — ne pas éditer
│   ├── js/
│   └── uploads/                # ⚠️ Fichiers uploadés — non versionnés
│
├── Migrations/                 # Migrations EF Core — versionnées
│
├── appsettings.json            # Config de base — versionné
├── appsettings.Example.json    # Template à copier — versionné
├── appsettings.Development.json # ⚠️ Config locale — non versionné
│
├── package.json                # Dépendances Tailwind
├── tailwind.config.js          # Configuration Tailwind
└── README.md
```

---

## Scripts NPM

| Commande        | Description                            |
|-----------------|----------------------------------------|
| `npm run dev`   | Tailwind en mode watch (développement) |
| `npm run build` | Compilation Tailwind (production)      |

---

## Migrations
```bash
# Créer une nouvelle migration après modification d'un modèle
dotnet ef migrations add NomDeLaMigration

# Appliquer les migrations en attente
dotnet ef database update

# Annuler la dernière migration (si pas encore appliquée)
dotnet ef migrations remove

# Lister toutes les migrations et leur état
dotnet ef migrations list

# Revenir à une migration spécifique
dotnet ef database update NomDeLaMigration
```

> ⚠️ **Ne jamais supprimer** le dossier `Migrations/` — il permet à tous les devs de recréer la base identique.

---

## Problèmes fréquents

### ❌ `output.css` manquant — styles absents

Le CSS Tailwind n'a pas été compilé.
```bash
npm install
npm run build
```

### ❌ Erreur de connexion à la base de données

Vérifiez votre `appsettings.Development.json` :
```bash
# Tester la connexion LocalDB
sqllocaldb info MSSQLLocalDB

# Démarrer l'instance si arrêtée
sqllocaldb start MSSQLLocalDB
```

### ❌ Migrations non appliquées
```bash
dotnet ef database update
```

### ❌ `dotnet ef` introuvable

L'outil EF Core CLI n'est pas installé globalement.
```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

### ❌ Port déjà utilisé

Modifiez les ports dans `Properties/launchSettings.json` :
```json
"applicationUrl": "https://localhost:7001;http://localhost:5001"
```

### ❌ Certificat HTTPS non approuvé
```bash
dotnet dev-certs https --trust
```

---

## Contribution

### Branches

| Branche    | Rôle                              |
|------------|-----------------------------------|
| `main`     | Code stable — production          |
| `develop`  | Intégration des features          |
| `feature/` | Nouvelles fonctionnalités         |
| `fix/`     | Corrections de bugs               |

### Workflow
```bash
# 1. Partir de develop
git checkout develop
git pull origin develop

# 2. Créer une branche
git checkout -b feature/ma-fonctionnalite

# 3. Développer, puis commiter
git add .
git commit -m "feat: description claire du changement"

# 4. Pousser
git push origin feature/ma-fonctionnalite

# 5. Ouvrir une Pull Request vers develop
```

### Convention des commits

| Préfixe    | Usage                              |
|------------|------------------------------------|
| `feat:`    | Nouvelle fonctionnalité            |
| `fix:`     | Correction de bug                  |
| `style:`   | Modifications CSS / UI             |
| `refactor:`| Refactoring sans changement de comportement |
| `chore:`   | Maintenance, dépendances           |
| `docs:`    | Documentation                      |

---

## Licence

MIT — voir [LICENSE](LICENSE)