# ProductQRCodeReadWithDynamic

- This project is a real-time and qrcode focused project developed with .Net Core MVC.



## Features Available in the Project

- User authentication with Login/Register
- QrCode creation
- Real-Time QrCode reading
- Real-Time e-mail sending
- Real-Time product update
- Real-Time notification management
- Qrcode reading, update, QrCode verification with camera
- User or guest tracking connected to the system




  
## Run on Your Computer

Clone the project

```bash
  git clone https://github.com/kadirdemirkaya/ProductQRCodeReadWithDynamic.git
```

Then change the url or information appropriate to your server from the .json extension files

```bash
  appsettings.json
      - SqlConnectionString:
      - EmailConfiguration:
```

Go to the project directory and create and save the migration for the database

```bash
  cd /ProductQRCodeReadWithDynamic
  dotnet ef migrations add 'your_migration_name'
  dotnet ef database update
```

Run the server

```bash
  dotnet run
```

## Screenshots of the Project

### - In this section, a project-specific QrCode can be created in the detail section.
![CodeCreate](https://github.com/kadirdemirkaya/ProductQRCodeReadWithDynamic/assets/126807887/aedc7426-c133-46ed-b453-ce4737c9cf96)

### - In this section, the code of a random product is read from the "ReadCode" section and you are directed directly to the product detail page.
![CamReadCode](https://github.com/kadirdemirkaya/ProductQRCodeReadWithDynamic/assets/126807887/07a4b755-ea13-434b-83f7-5f1a56bca993)

### - If you want to update the product in this section, an e-mail is sent to the logged in account and verification is made by scanning the QrCode in the picture and the product update is completed successfully.
![EditMail](https://github.com/kadirdemirkaya/ProductQRCodeReadWithDynamic/assets/126807887/613f8173-e297-4781-b877-1aea14d0e577)
![MailCode](https://github.com/kadirdemirkaya/ProductQRCodeReadWithDynamic/assets/126807887/592689cb-2056-4c5c-902a-06111569e8d3)
![ProductUpdateForCode](https://github.com/kadirdemirkaya/ProductQRCodeReadWithDynamic/assets/126807887/ea5d03ab-76da-4092-b48c-0852ea77700d)
