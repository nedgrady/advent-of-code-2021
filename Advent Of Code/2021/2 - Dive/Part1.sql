DROP TABLE IF EXISTS dbo.Command
GO

CREATE TABLE dbo.Command
(
	Type nvarchar(10) NOT NULL,
	Argument int NOT NULL
)
GO

TRUNCATE TABLE dbo.Command
GO

BULK INSERT dbo.Command  FROM 'C:\Code\advent-of-code-2021\2 - Dive\input.txt' 
   WITH (FIELDTERMINATOR = ' ',  ROWTERMINATOR='\n')
GO
   
;with cteTotals as
(
	SELECT	Type,
			CASE WHEN Type = N'up' then -SUM(Argument) ELSE SUM(Argument) END Total
	FROM	dbo.Command
	GROUP BY	Type
)
SELECT
(
	SELECT	SUM(Total)
	FROM	cteTotals
	WHERE	Type <> N'forward'
) *
(
	SELECT	Total
	FROM	cteTotals
	WHERE	Type = N'forward'
)