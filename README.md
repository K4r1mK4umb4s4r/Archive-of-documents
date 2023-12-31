# "Архив документов"
![image](https://github.com/K4r1mK4umb4s4r/Archive-of-documents/blob/main/image.png)
### Регистрация пользователя

- Пользователи должны иметь возможность регистрироваться в системе.
- При регистрации требуется указать следующие данные:
  - Логин (уникальный)
  - Пароль
  - Электронная почта (уникальная)
  - Имя пользователя (необязательно)
- Система должна проводить проверку уникальности логина и электронной почты.
- Пароль должен храниться в безопасной форме с использованием хэширования.

### Логин и логаут пользователя

- Зарегистрированные пользователи должны иметь возможность входить в систему, используя свой логин и пароль.
- Пользователи должны иметь возможность выходить из системы (логаут).

### Просмотр списка записей (INDEX)

- Зарегистрированные пользователи (включая администраторов и обычных пользователей) должны иметь доступ к списку всех записей в архиве.
- Список записей должен быть отсортирован и предоставлять возможность фильтрации и поиска.

### Создание записи (CREATE)

- Зарегистрированные пользователи (включая администраторов и обычных пользователей) должны иметь возможность создавать новые записи в архиве.
- При создании записи следует указывать следующие поля:
  - Название
  - Описание (необязательно)

### Просмотр деталей записи (READ)

- Зарегистрированные пользователи (включая администраторов и обычных пользователей) должны иметь доступ к подробной информации о каждой записи.
- Пользователи должны иметь возможность просматривать содержимое документа и другие метаданные записи.

### Редактирование записи (UPDATE)

- Редактирование и удаление записей должны быть доступны только администраторам.
- Администраторы должны иметь возможность изменять информацию о существующих записях, включая изменение названия и описания записи.

### Удаление записи (DELETE)

- Удаление записей должно быть доступно только администраторам.
