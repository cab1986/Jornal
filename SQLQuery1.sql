SELECT '������' ������������,count(mark) ���������� 
FROM mark
UNION ALL
SELECT '�����', count(id_les) ���������� FROM lesson;

SELECT 
	CASE
    WHEN prava=1 THEN '�������������'
    WHEN prava=2 THEN '�������'
    WHEN prava=3 THEN '������'
	END  ���������, 
	fio_user ���
FROM users ORDER BY id_user, fio_user;

SELECT predmet.predmet �������,
		(SELECT fio_user FROM users WHERE users.id_user=t_st_pred.id_tch) �������,
		(SELECT fio_user FROM users WHERE users.id_user=t_st_pred.id_st) ������, 
		(SELECT count(id_les) FROM lesson WHERE t_st_pred.id_st=lesson.id_st AND t_st_pred.id_pred=lesson.id_pred  AND t_st_pred.id_tch=lesson.id_tch) ���_��_������, 
		(SELECT count(mark.id_m) FROM mark WHERE t_st_pred.id_st=mark.id_st AND t_st_pred.id_pred=mark.id_pred) ���_��_������
FROM t_st_pred
	JOIN predmet ON predmet.id_predmet=t_st_pred.id_pred
	JOIN users ON users.id_user=t_st_pred.id_tch ;