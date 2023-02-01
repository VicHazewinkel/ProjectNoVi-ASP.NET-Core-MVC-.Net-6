<h1>Project NoVi</h1>
Website voor het managen van de catalogus van optiek NoVi. 
<br>
<h2>API</h2>
<h3>Get</h3>
url: https://localhost:7233/api/products
<h3>Post</h3>
url: https://localhost:7233/api/products<br>
json: <br>
{<br>
    "type": "Sunglasses",<br>
    "brandName": "Theo",<br>
    "collection": "Marfona",<br>
    "color": "Hout Kleurig",<br>
    "material": "Hout",<br>
    "price": 650,<br>
    "correction": 0<br>
}<br>
<h3>Delete</h3>
url: https://localhost:7233/api/products<br>
parmms: KEY "id"  |  VALUE: "6"
<h3>Update</h3>
url: https://localhost:7233/api/products<br>
parmms: KEY "id"  |  VALUE: "5"<br>
Body: <br>
{<br>
    "type": "Sunglasses",<br>
    "brandName": "Theo",<br>
    "collection": "Forest",<br>
    "color": "Hout Kleurig",<br>
    "material": "Bamboe ",<br>
    "price": 300,<br>
    "correction": 0<br>
}<br>

<h2>Features</h2>
<ul>
	<li>Catalogus browsen </li>
	<li>Item beheer</li>
	<li>Merk beheer</li>
	<li>User beheer</li>
	<li>Account aanmaken</li>
	<li>Account beheer</li>
</ul>
<h2>Gebruikte technologie</h2>
<ul>
	<li>Gebruiken van rollen voor authenticatie en autorisatie </li>
	<li>RestFull API Toegepast op de CRUD-bewerkingen</li>	
	<li>Seeding Producten, Merken, Users en Rollen</li>
	<li>Middleware</li>
	<li>ASP.NET Core 6.0</li>
	<li>Entity Framework Core</li>
	<li>Identity Framework</li>
	<li>Localization</li>
	<li>Meertaligheid</li>
</ul>
<h2>Website Gebruiksaanwijzing</h2>
<h3>Gebruiker</h3>
Gebruiker kan browsen door het aanbod van optische Hulpmiddelen, Zonnebrillen en Lenzen. 
<h3>Aangemelde Optometrist</h3>
De Optometrist kan hetzelfde als de gebruiker met extra functionaliteiten. 
<br>
Producten toevoegen, wijzigen en verwijderen. 
<br>
Merken toevoegen, wijzigen en verwijderen.
<h3>Admin</h3>
De Admin heeft dezelfde functionaliteiten als de gebruiker met extra features.
<br>
Producten en Merk overzicht
<br>
Beheren van Gebruikers en rollen
<br>
<h3>Login voor 3 Test gebruikers met toebehoren rollen</h3>
<h5>User</h5>
email: User@gmail.com 
<br>
paswoord: Abc@123

<h5>Admin</h5>
email: Admin@gmail.com 
<br>
paswoord: Abc@123

<h5>Optometrist</h5>
email: Optometrist@gmail.com 
<br>
paswoord: Abc@123
<br>
<h2>Gebruikte Bronnen</h2>
<ul>
	<li>Udemy Courses</li>
	<li>Skillshare Courses</li>
	<li>Bootstrap</li>
</ul>

