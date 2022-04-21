SELECT 'Оценки' Деятельность,count(mark) Количество 
FROM mark
UNION ALL
SELECT 'Уроки', count(id_les) Количество FROM lesson;

SELECT 
	CASE
    WHEN prava=1 THEN 'Администратор'
    WHEN prava=2 THEN 'Учитель'
    WHEN prava=3 THEN 'Ученик'
	END  Должность, 
	fio_user ФИО
FROM users ORDER BY id_user, fio_user;

SELECT predmet.predmet Предмет,
		(SELECT fio_user FROM users WHERE users.id_user=t_st_pred.id_tch) Учитель,
		(SELECT fio_user FROM users WHERE users.id_user=t_st_pred.id_st) Ученик, 
		(SELECT count(id_les) FROM lesson WHERE t_st_pred.id_st=lesson.id_st AND t_st_pred.id_pred=lesson.id_pred  AND t_st_pred.id_tch=lesson.id_tch) Кол_во_уроков, 
		(SELECT count(mark.id_m) FROM mark WHERE t_st_pred.id_st=mark.id_st AND t_st_pred.id_pred=mark.id_pred) Кол_во_оценок
FROM t_st_pred
	JOIN predmet ON predmet.id_predmet=t_st_pred.id_pred
	JOIN users ON users.id_user=t_st_pred.id_tch ;