Create view Пользователи as
SELECT  id_user, username as 'Имя', email as 'Почта', password as 'Пароль', disabled_person as 'Инвалид', name_role as 'Роль'
FROM Users
INNER JOIN Roles ON (Users.id_role = Roles.id_role) 
