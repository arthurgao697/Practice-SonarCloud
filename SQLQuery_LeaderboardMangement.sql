CREATE TABLE Project00_Wordle.Leaderboards
(
    ID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(64) NOT NULL,
    Used_Turns INT NOT NULL,
    Used_Time INT NOT NULL
);

-- DROP TABLE Project00_Wordle.Leaderboards;

INSERT INTO Project00_Wordle.Leaderboards (Username, Used_Turns, Used_Time)
VALUES ('TEST1', 4, 72);

INSERT INTO Project00_Wordle.Leaderboards (Username, Used_Turns, Used_Time)
VALUES ('TEST2', 6, 53);

SELECT TOP 10 *
FROM Project00_Wordle.Leaderboards
ORDER BY Used_Turns ASC;

SELECT TOP 10 *
From Project00_Wordle.Leaderboards
ORDER BY Used_Time ASC;

-- DELETE FROM Project00_Wordle.Leaderboards;