# ProCreJect 
 ```
Задание: Регистриране на windows service(Optional functionality event handler when new swift 
message drop) за обработка на SWIFT-Society for Worldwide Interbank Financial Telecommunications 
message (type MT799 Free Format Message), находящо в конкретна директория и извличане на 
определени данни от него. След успешно извличане се записват в база данни.
 ```
 
 ## First module (Shredding):
 ```
Разделяне на swift съобщението ShreddingFile.ShrederSWIFTFile():
Message and TextBlock(body) - като TextBlock се extract-ва от message (ShreddingFile.ShrederBody()).
Вмъкват се в подходяща структура от данни.
 ```
 
 ## Second module watch for changes in a specified directory (WinServiceHandleSwiftMessage.Start())
 ```
 Поставане на файловете в определена директория. Event handling when new swift message drop.
 Raising event when new swift message arrived. 
 Обработка на съобщението (Shredding);
 След това съобщението се предава на подходящата database комуникация и се записва.
 ```

 
 ## Third module database processing
```
Create and build connection to database.
Database factory - избиране на подходяща базаданни (Relational Database or NoSQL Database)
За избор съм направим MSSQL, а за CRUD заявки dapper framework. 
String connection взимам от App.config с ConfigurationManager и я подавам на SqlConnection
Класа DataInsert може да послужи за repository. Имплементирал съм само insert, с малка server-side validation
Също така съм направим DTO Message с връзка one-to-many към DTO TextBlock FKey e id на TextBlock и insert-вам първо TextBlock, извличам
TextBlockId, за да може да присвоя на полето Message.TextBlockId и така инсертвам втората част от swift съобщението;
```  

## Fourth module win service
```
Създаване и регистриране на win service.
```

###  Used tools
> FileSystemWatcher
> Dapper
> Topshelf
