# Test Task
Веб застосунок відображає каталоги які розміщені в БД. Також є функція імпорту/експорту каталогів з/в БД.

![image](https://user-images.githubusercontent.com/102614143/235213433-435b3320-b4f0-4272-82f2-ec1df391bdad.png)

Після запуску проєкта відкривається сторінка на якій ви можете побачити струтуру із картинки вище

![image](https://user-images.githubusercontent.com/102614143/235214019-7166dc68-c6da-4025-8aa9-aab26acda215.png)
Ви маєте можливість переміщатися по підкаталогам

Наприклад:

![image](https://user-images.githubusercontent.com/102614143/235214223-eef5eb04-e577-489b-8fba-1e70ca9e7c0f.png)

## Експорт
Якщо натиснути `export this folder to json` каталог в якому ви знаходитесь експортується в JSON файл та завантажиться.

Нижче приклад структури експортованого файлу:

![image](https://user-images.githubusercontent.com/102614143/235214797-c0206a7c-23ea-4997-bc1a-828700e8907b.png)

## Імпорт
Натиснемо кнопку `Browse...` та оберемо JSON файл який хочемо експортувати

![image](https://user-images.githubusercontent.com/102614143/235215128-b2e88fcf-9a46-46cb-80a3-24e5a81a5324.png)

Після натискаємо кнопку `Upload File`

Після імпорту ви можете побичити як новий каталог додався

![image](https://user-images.githubusercontent.com/102614143/235215766-b6ec09a1-88bf-4faa-8320-983bbec1f1b2.png)

ВАЖЛИВО: В кожного каталога та підкаталога повині бути унікальні ID , інакше файл просто не імпортується

# :floppy_disk: Встановлення
Перед запуском програми ми повинні створити базу даних. В цьому проєкті використовував Postgresql.

Для початку потрібно зайти в СУБД та створити базу даних я використовую psql 
тому після логіна ввожу команду `CREATE DATABASE testtask`.

![image](https://user-images.githubusercontent.com/102614143/235216876-3cb0153d-1727-40c0-a85c-374c15ddd737.png)

Після за допомогою команди `dotnet ef database update` оновлюємо базу даних до останньої міграції.

Якщо в вас не працює ця команда спробуйте наступне:
- встановити Entity Framework Core tools командою `dotnet ef database update`
- змінити ConnectionString

Після створення міграції можна перевірити чи створилась таблиця в базі даних командою `select * from catalogs;`

![image](https://user-images.githubusercontent.com/102614143/235219477-4d14c7d6-4919-44ed-9361-c1c74b32c60e.png)

Після цього можна запускати проект. Потрібно відкрити папку TestTask в терміналі, та написати команду `dotnet run`

Додавання базового наповнення відбувається автоматично.

![image](https://user-images.githubusercontent.com/102614143/235220288-f1d0312d-2744-456c-ad05-7b5513f22d5b.png)


# Technology stack
- ASP.NET Core 6 MVC
- PostgreSQL and EF 6

Features:
- [x] Відображення каталогів на сторінці
- [x] Імпорт каталогів з JSON в БД
- [x] Експорт каталогів в JSON

