Данная инструкция актуальня для операционной системы Ubuntu
Для работы клиентским с приложением нужно:
* Установить node.js (npm). Как это сделать, можно узнать [по ссылке](https://losst.ru/ustanovka-node-js-ubuntu-18-04);
* Установить шрифт Product Sans (доступен на сайте Google);
* Запустить приложение с консоли находясь в папке с приложением (где лежит файл ``webpack.config.js``);

Это можно сделать, введя следующие команды:
````
    npm install
    npm run dev
````
Далее приложение запустет сервер на локальном порту ``8080``. Убедитесь, что другие приложения не запущены на этом порту.
Перейдите в браузер на страницу ``localhost:8080/``, приложение откроется.
Это сделано с целями правильной работы тестов.

Для работы с сервером нужно:
* Установить PostgreSQL 9.6
* Установить dotNet

Установив данные программы, перейти к папке с init.sql и выполнить команду:
````
    psql -h localhost -p 5433 -U postgres projectstoredb < init.sql
````
Далее перейти в папку ``api/Authentification`` и выполнить команду:
````
    dotnet run
````
Приложение запустится на порту ``5000``

Для работы тестов нужно установить python, библиотеку selenium и chromedriver
* Для установки python нужно запустить команду:
````
    supo apt-get install python3.7
````
Понадобится доступ в интернет.
* Далее нужно установить библотеку selenium на python3.7:
````
    /usr/bin/python3.7 -m pip install selenim
````
* Далее нужно установить ``chromedriver``. Скачать его можно по ссылке. Обратите внимание, что для каждой из последних 
нужен свой [chromedriver](http://chromedriver.chromium.org/downloads);
* Далее скачав ``chromedriver`` и распаковав содержимое архива (исполняемый файл), нужно переместить его в ``/usr/bin/``.
Запустив терминал в одной папке с ``chromedriver``, выполните команду:
````
    sudo cp chromedriver /usr/bin/
```` 
* Далее нужно сделать chromedriver исполняемым:
````
    sudo chmod +x /usr/bin/chromedriver
````
* Теперь можно запустить скрипты тестов. Их нужно выполнять в строгом порядке. Запуск тестов в произвольном порядке не
предусматривался. Находясь в одной папке со скриптами, выполняйте команды в следующем порядке, дожидаясь выполнения
каждого скрипта:
````
  /usr/bin/python3.7 test1_admin.py
  /usr/bin/python3.7 test1_manager.py
  /usr/bin/python3.7 test1_worker.py
````
* Порядок выполнения следующих скриптов неважен:
````
    /usr/bin/python3.7 test_diagram.py
    /usr/bin/python3.7 test_gitHub.py
````