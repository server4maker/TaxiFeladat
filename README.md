OTP Feladat – Járműajánló Web API

Ez a projekt egy ASP.NET Core Web API, amely útvonalhoz ajánl optimális járműkombinációkat megadott utasszám és távolság alapján. A cél, hogy segítse a diszpécsert a lehető legnagyobb nyereséggel járó járműkombináció kiválasztásában.

-------------

Projekt célja

A rendszer célja:
- A meglévő járműállományból összeállítani érvényes járműkombinációkat.
- Minden kombináció esetén kiszámítani a profitot.
- A járműveket és utazási paramétereket figyelembe véve javaslatot adni a diszpécsernek.
- További járműveket hozzáadni

-------------

Projekt struktúra

/Controllers → API végpontok
/Models → Adatmodellek (Vehicle, DTO-k)
/Interfaces → Absztrakciók (Service, Repository)
/Repositories → Adatkezelés EF Core-al
/Services → Üzleti logika

-------------

Használt technológiák:

ASP.NET Core Web API - Backend keretrendszer
Entity Framework Core - ORM + SQLite
SQLite - Egyszerű relációs adatbázis
Swagger

-------------

Működési logika:

A TripsController szolgáltatásában a felhasználó megad:
- utasok száma (pl. 6)
- távolság (pl. 80 km)

Ez alapján a rendszer:
1. Lekérdezi az összes járművet az adatbázisból.
2. Létrehozza az összes lehetséges járműkombinációt (maximum 3 jármű egy kombóban).
3. Szűri a kombinációkat az alábbi szabályok alapján:
   - Összesített férőhely ≥ utasok száma
   - Összesített hatótáv elegendő az út megtételéhez (figyelembe véve a MildHybrid speciális fogyasztását)
4. Minden kombinációhoz kiszámítja a profitot:
   - Bevétel: utasok száma × 2 × távolság
   - Idődíj: félórás blokkok × utasok száma × 2
   - Költség: járművenként elektromos = 1 egység/km, más = 2 egység/km
   - Profit = bevétel - költség
5. A legmagasabb profitú kombináció alapján csökkenő sorrendben visszaküldi a lehetséges megoldásokat.

-------------

Adatmodellek

Vehicle
public class Vehicle
{
    public int Id { get; set; }
    public int PassengerCapacity { get; set; }
    public int Range { get; set; }
    public FuelType Fuel { get; set; }
}

-------------

Adatbázis

SQLite adatbázist használunk, amit EF Core DbContext kezel:

public class ApplicationDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle { Id = 1, PassengerCapacity = 4, Range = 250, Fuel = FuelType.Gasoline },
            new Vehicle { Id = 2, PassengerCapacity = 3, Range = 120, Fuel = FuelType.MildHybrid },
            new Vehicle { Id = 3, PassengerCapacity = 2, Range = 100, Fuel = FuelType.PureElectric }
        );
    }
}

-------------

Migrációk

Add-Migration InitialCreate
Update-Database

Minden modellváltozás után új migráció szükséges.


-------------

API végpontok

GET	/api/vehicles	Összes jármű listázása
POST	/api/vehicles	Új jármű felvétele
GET	/api/trips/suggestions	Járműajánlat generálása

-------------

Swagger

Automatikusan elérhető az alkalmazás indítása után:
https://localhost:port/swagger
Itt interaktívan lehet:
-Járművet hozzáadni
-Ajánlatokat tesztelni
-JSON válaszokat látni

-------------

Tervezési döntések:
Interface alapú rétegek: Tesztelhetőség és bővíthetőség miatt minden réteghez készült interfész (ITripSuggestionService, IVehicleRepository).
DTO használat: Az entitásokat nem adjuk vissza közvetlenül a kliensnek.

-------------

A project elindítása:

1.Követelmények:
-Telepítve legyen a Visual Studio 2022 (vagy újabb), amely támogatja a .NET 8.0-t.
-Telepítve legyen a .NET 8.0 SDK.
2.Adatbázis előkészítése:
-A projekt SQLite adatbázist használ, az Entity Framework Core automatikusan létrehozza az adatbázist és a szükséges táblákat, ha azok még nem léteznek.
-Ha változtattál az adatmodellben, futtatni kell a migrációkat a „Package Manager Console”-ban:
Add-Migration InitialCreate
Update-Database
3.Projekt futtatása:
-Visual Studio-ban nyomd meg az F5-öt, vagy kattints a „Start Debugging” gombra.
-A backend egy helyi szerveren fog futni (pl. https://localhost:5001).
4.API dokumentáció (Swagger) elérése:
-A fejlesztői környezetben elérhető a Swagger UI
-Automatikusan elindul

-------------

Készült:
Ez a projekt egy backend-pozícióra való állásinterjúra készült, demonstrálva:
-a rétegezett backend architektúrát
-API fejlesztést
