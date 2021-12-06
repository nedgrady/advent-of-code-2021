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

DECLARE
	@CurrentAim bigint = 0,
	@CurrentVerticalPosition bigint = 0,
	@CurrentHorizontalPosition bigint = 0

SELECT
	@CurrentAim =
		CASE
			WHEN Type = N'Up' THEN @CurrentAim - Argument
			WHEN Type = N'Down' THEN @CurrentAim + Argument
			ELSE @CurrentAim
		END,
	@CurrentVerticalPosition =
		CASE
			WHEN Type = N'Forward' THEN @CurrentVerticalPosition + (@CurrentAim * Argument)
			ELSE @CurrentVerticalPosition
		END,
	@CurrentHorizontalPosition =
		CASE
			WHEN Type = N'Forward' THEN @CurrentHorizontalPosition + Argument
			ELSE @CurrentHorizontalPosition
		END
FROM	dbo.Command

SELeCT @CurrentVerticalPosition * @CurrentHorizontalPosition [Solution]

