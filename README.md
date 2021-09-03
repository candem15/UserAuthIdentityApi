
<details>
  <summary>Table of Contents</summary>
  
  1. <a href="#About Project">About Project</a>
      * <a href="#Built With">Built With</a>
  2. <a href="Stages of Project">Stages of Project</a>
     * <a href="Create Asp.Net Core MVC Application and Scaffolding the Identity UI">Create Asp.Net Core MVC Application and Scaffolding the Identity UI</a>
     * <a href="Create ApplicationUser Inherited from IdentityUser">Create ApplicationUser inherited from IdentityUser</a>
     * <a href="Customize Register and Login Pages">Customize Register and Login Pages</a>
     * <a href="Managing(Create, Read, Update, Delete) Roles">Managing(Create, Read, Update, Delete) Roles</a>
     * <a href="Listing and Managing User's Roles">Listing and Managing User's Roles</a>
</details>

# <p id="About Project">About Project</p>

UserAuthIdentityApi is customized version of Asp.Net Core MVC alongside Identity UI. Besides default features this includes bunch of additional user details, role management, 
custom SignIn/SignUp pages and much more. 
This project covers most of the practical use cases involved while developing User Management and Authentication in ASP.NET Core.

## <p id="Built With">Built With</p>
<div>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width=75px>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-plain.svg" width=75px>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-original-wordmark.svg" width=75px>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/html5/html5-plain-wordmark.svg" width=75px>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/css3/css3-plain-wordmark.svg" width=75px>
</div>

# Stages of Project

## Create Asp.Net Core MVC Application and Scaffolding the Identity UI

Start off with creating ASP.NET Core MVC project with with Authenication. I'm used VScode as IDE so i will show it via terminal commands.
```
Create empty Asp.Net Core MVC App with Authentication => dotnet new mvc -o UserAuthIdentityApi
```
```
Include Authentication => dotnet new mvc --auth Individual -- force
```
Add if you wanna use Git
```
Create new repository => git init
Git ignore for specily Asp.Net Core Apps => dotnet new gitignore
```
Now we have to scaffold the identiy ui to project that we customize later on. 
But before scaffolding process we also need to install couple of packages.
```
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
After intall packages now we can scaffold identity ui
```
dotnet aspnet-codegenerator identity -dc UserAuthIdentityApi.Data.ApplicationDbContext
```
Once it is added, we can see a number of razor pages in the Areas folder. 
These are files that act as the default Identity UI. 
Moving further we will customizing Identity to match our requirements for this project.
After scaffolding, project folder will look like this ;

<img src="https://i.imgur.com/NiSbED2.png" width=300px alt="Identity Scaffold">

## Create ApplicationUser inherited from IdentityUser

I recommend to install this package for to see frontend changes without restarting application.
```
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation --version 5.0.9
```
Then apply this service in Startup.cs at ConfigureServices method.
```
services.AddControllersWithViews().AddRazorRuntimeCompilation();
```
As default IdentityUser have 10-15 columns that we can work on. But in this project we want add bunch of more propeties to User so that will become more detailed profile.
At Data folder created ApplicationUser.cs then inherited from IdentityUser after that added properties that we want to use.
```
public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="text")]
        public string FirstName{get; set;}

        [PersonalData]
        [Column(TypeName ="text")]
        public string LastName{get; set;}

        [PersonalData]
        [Column(TypeName ="text")]
        public string Country{get; set;}

        [PersonalData]
        [Column(TypeName ="integer")]
        public int Age{get; set;}

        [PersonalData]
        public byte[] ProfilePicture { get; set; }
    }
```
Since we decided to change the default User class from IdentityUser to ApplicationUser, we had to make other changes in our existing code as well. With that changes, we don't have to go all scaffolded files and make changes manually.
*At Startup.cs/ConfigureServices
```
DbContext => ApplicationDbContext
IdentityUser => ApplicationUser
```
*At ApplicationDbContext
```
IdentityUser => ApplicationUser
```

## Migration and Update Database

Before creating new migrations, we deleted existing migrations that came from creating project. 
At ApplicationDbContext.cs we overrided existing columns to new columns names that we want. After that change table names: "AspNetUsers" => "Users", "AspNetRoles" => "Role" ...
```
protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("AuthMVC");
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "Users");
        });
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
    }
```
I was used PostgreSql as database so i will show for configurations for PostgreSql.
You can use different databases it is also possible but i recommend to check db compatibility.
Next, for migrate PostgreSql we have to run following commands on terminal.
```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design
```
After runned these commands we added new ConnectionString for Postgresql at appsettings.json then used in Startup.cs instead of default one.
```
"ConnectionStrings": {
    "DefaultConnection": "DataSource=app.db;Cache=Shared",
    "PostgresqlAuthConnection":"Server=127.0.0.1;Port=5432;Database=AuthMVC;User Id=postgres;Password=112233;"
  }
```
Now we able to do create migration and update our database with that migrations files.
```
dotnet ef migrations add firstMigration
dotnet ef database update
```
Now our database ready to be running on PostgreSQL. 
<div>
<img src="https://i.imgur.com/pbhekeH.png" width=200px alt="postgres">
</div>

## Customize Register and Login Pages

Here i wanted to see both pages like switch buttons in same template. 
This part is about designing pages so its completely optional.
I added first name and last name section for registering new users. Further design codes can be found in Areas/Identity/Pages/Account/Register.cshtml and Login.cshtml 
<div>
<img src="https://i.imgur.com/CKm0GUT.png" width=300px alt="Login">
<img src="https://i.imgur.com/nUV9va5.png" width=300px alt="Register">
</div>
In Register.cshtml.cs file added FirstName and LastName to InputNodel class with Required attritibute. 
Also at Login.cshtml.cs maded changes that will allow to login with both username and email for user. Further explanations can be found at same file's comment sections.

## Customize User Fields at Profile Settings

After signing in on application at right corner(navbar) we can able to see our UserName like "Hi UserName!". 
By click our username this will be show us our account's management pages. 
Here(Areas/Identity/Pages/Account/Manage/Index.cshtml) we will add custom fields that we created at ApplicationUser.cs file. 
Firstly, we expanted existing fields and maded referencal changes. 
Adding picture is little more complicated. 
At "Index.cshtml.cs" in OnPostAsync method we will save picture that uploaded by user.
```
if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }
```
Again Index.cstml contains design and explanation with comment lines inside. 
Summarize; If the user has an image uploaded to the database already, we get the byte array from the model and convert it to an image.
Then this image will be displayed on the page. 
Else, shows a blank image container. 
Also there is input button that allows upload picture from your box.
Like some social network applications, i wanted to add that picture as logo at navbar menu.
Go throught Views/Shared/_LoginPartial.cshtml and add new item to navbar
```
<li class="nav-item" style="align-self: center;">
        @if (UserManager.GetUserAsync(User).Result.ProfilePicture != null)
        {
            <img style="width:40px;height:40px; object-fit:cover; border-radius:30px" src="data:image/*;base64,@(Convert.ToBase64String(UserManager.GetUserAsync(User).Result.ProfilePicture))">
        }
 </li>
 ```
<img src="https://i.imgur.com/BBUIFpU.png" width=750px alt="profile">

## Managing(Create, Read, Update, Delete) Roles

Now moving to create role based authorizing for reaching web pages. 
And theese roles will attached to users and works like military ranks. 
Each role will have certain level of permission to make action on application. 
First of all, we will seed default roles to database. 
This seed will helps us to assign role auto when new account created.
Create an Enum for the supported Roles. Add a new Enum at Enums/Roles.cs
```
public enum Roles
{
    SuperAdmin,
    Admin,
    Moderator,
    Basic,
    Visitor
}
```
Then add ContextSeed.cs in Data folder and add Roles that we created at Enums/Roles.cs with RoleManager's CreateAsync method under SeedRolesAsync task method.
Now seeded roles must be invoke at somewhere. In mine app, this place is main function at Program.cs
```
await ContextSeed.SeedRolesAsync(userManager, roleManager);
```
We can also seed user with SuperAdmin role that have every permission over application like owner of it. 
But its optional. 
If you want to seed that go through ContextSeed.cs => SeedSuperAdminAsync method. 
Kinda have same procedure like seeding default roles.
For assigning role to newly created accounts, we adding one line of code at Register.cshtml.cs
```
await _userManager.AddToRoleAsync(user, Enums.Roles.Basic.ToString());
```
After making our seeds, start building an UI with which we can View and Manage Roles. 
It will be a simple view that Lists out all the roles available and has a feature to CRUD operations. 
Start by adding a new Empty MVC Controller to the Controllers folder and naming it "RoleController.cs". 
Here we generated our CRUD operations and will be shown at its view(Views/Role/Index.chtml). 

<img src="https://i.imgur.com/SnsxZVx.png" alt="Manage roles">

## Listing and Managing User's Roles


