
-- Contar la cantidad de tareas asignadas a cada usuario:
SELECT u.id, u.username, COUNT(t.id) AS task_count
FROM user u
LEFT JOIN task t ON u.id = t.user_id
GROUP BY u.id, u.username;

-- Find the tasks (tasks) that have an upcoming due date (due_date) (for example, in the next 7 days):
-- MySQL
SELECT *
FROM task
WHERE due_date >= DATE(NOW()) AND due_date <= DATE_ADD(DATE(NOW()), INTERVAL 7 DAY);

-- SQLite
SELECT *
FROM task
WHERE due_date >= DATE('now') AND due_date <= DATE('now', '+7 days');


-- Calculate the average duration of tasks (difference between creation date and due date):
-- MySQL
SELECT AVG(DATEDIFF(due_date, created_date)) AS avg_duration
FROM task
WHERE due_date IS NOT NULL;

-- SQLite
SELECT AVG(julianday(due_date) - julianday(created_date)) AS avg_duration
FROM task
WHERE due_date IS NOT NULL;


-- Most recent tasks: Get the 5 most recent tasks sorted by creation date.
SELECT *
FROM task
WHERE user_id IS NULL;


-- Show users along with the number of tasks they have in “in_progress”, “pending” or “completed” status,
SELECT u.username,
       COALESCE(in_progress_count, 0) AS in_progress_count,
       COALESCE(pending_count, 0) AS pending_count,
       COALESCE(completed_count, 0) AS completed_count
FROM user u
         LEFT JOIN (
    SELECT user_id,
           COUNT(CASE WHEN status = 'in_progress' THEN 1 END) AS in_progress_count,
           COUNT(CASE WHEN status = 'pending' THEN 1 END) AS pending_count,
           COUNT(CASE WHEN status = 'completed' THEN 1 END) AS completed_count
    FROM task
    GROUP BY user_id
) t ON u.id = t.user_id;

-- Count how many tasks of each priority each user has and display the total sum of their tasks,
SELECT u.id, u.username,
       COALESCE(SUM(CASE WHEN t.priority = 'high' THEN 1 ELSE 0 END), 0) AS high_priority_count,
       COALESCE(SUM(CASE WHEN t.priority = 'medium' THEN 1 ELSE 0 END), 0) AS medium_priority_count,
       COALESCE(SUM(CASE WHEN t.priority = 'low' THEN 1 ELSE 0 END), 0) AS low_priority_count,
       COUNT(t.id) AS total_tasks
FROM user u
         LEFT JOIN task t ON u.id = t.user_id
GROUP BY u.id, u.username;
