Если кто-то захочет разобрать то вот сурсы моей защиты для мап
Так же не много надо будет изменить ядро игры (Oxide) добавив один хук

Надо изменить метод
WorldSetup.InitCoroutine


И добавить вот такую строчку кода
Interface.CallHook("OnWorldLoad", global::World.Serialization);


Для тех кто захочет усилить защиту по префабам достаточно будет изменить класс Data либо изменить файл который генерируется в виде JSON и добавить в него больше префабов

https://media.discordapp.net/attachments/1093155753805758596/1147855647124242563/image.png?width=785&height=671
https://media.discordapp.net/attachments/1093155753805758596/1147855647451385856/image.png
