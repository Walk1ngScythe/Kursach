Create view ������������ as
SELECT  id_user, username as '���', email as '�����', password as '������', disabled_person as '�������', name_role as '����'
FROM Users
INNER JOIN Roles ON (Users.id_role = Roles.id_role) 
