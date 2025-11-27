
- Open Terminal as Administrator

Create Service:
sc.exe create "BasicService" binpath= "C:\Users\ITS\basicservice\BasicService\bin\Release\net9.0\publish\BasicService.exe"

Delete Service:
sc.exe delete "BasicService"


## Installazione dotNet Entity Framework
dotnet tool install --global dotnet-ef
## Aggiornamento
dotnet tool update --global dotnet-ef

# Creare una migration
dotnet ef migrations add "testo nota"

# Eliminare una migration
dotnet ef migrations remove

#Applicare migrations
dotnet ef database update

#Ricreare struttura classi a partire da db
dotnet ef dbcontext scaffold "Server=127.0.0.1;database=dblocale;user id=root;password=mypassword;port=3306;" Pomelo.EntityFrameworkCore.MySql

#Aggiornare struttura classi db indicando in quale path scrivere (--output-dir) e sovrascrivendo tutto (-f)
dotnet ef dbcontext scaffold "Server=127.0.0.1;database=dblocale;user id=root;password=mypassword;port=3306;" Pomelo.EntityFrameworkCore.MySql --output-dir Modules\LocalDB -f
