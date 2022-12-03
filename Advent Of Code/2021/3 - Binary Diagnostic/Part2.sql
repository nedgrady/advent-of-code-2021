DROP TABLE IF EXISTS dbo.DiagnosticReportEntry
GO

DROP TABLE IF EXISTS #CO2ScrubberDiagnosticEntries
GO

DROP TABLE IF EXISTS dbo.Numbers
GO
SELECT	ROW_NUMBER() OVER (ORDER BY @@SPID) AS Number
INTO	dbo.Numbers
FROM	sys.all_objects
GO

CREATE TABLE dbo.DiagnosticReportEntry
(
	Binary char(12) NOT NULL
)
GO

TRUNCATE TABLE dbo.DiagnosticReportEntry
GO

BULK INSERT dbo.DiagnosticReportEntry  FROM 'C:\Code\advent-of-code-2021\3 - Binary Diagnostic\input.txt'
   WITH (FIELDTERMINATOR = ' ',  ROWTERMINATOR='\n')
GO

SELECT	Binary
INTO	#CO2ScrubberDiagnosticEntries
FROM	dbo.DiagnosticReportEntry


DECLARE
	@NumberOfBits int = 12,
	@CurrentBitPosition int = 1

WHILE @CurrentBitPosition <= @NumberOfBits
BEGIN

	DELETE	dbo.DiagnosticReportEntry
	WHERE	SUBSTRING(Binary, @CurrentBitPosition, 1) <>
	(
		SELECT	TOP (1) SUBSTRING(Binary, @CurrentBitPosition, 1) [Bit]
		FROM	dbo.DiagnosticReportEntry
		GROUP BY	SUBSTRING(Binary, @CurrentBitPosition, 1)
		ORDER BY	COUNT(*) DESC,
					Bit DESC
	)

	DELETE	#CO2ScrubberDiagnosticEntries
	WHERE	SUBSTRING(Binary, @CurrentBitPosition, 1) <>
	(
		SELECT	TOP (1) SUBSTRING(Binary, @CurrentBitPosition, 1) [Bit]
		FROM	#CO2ScrubberDiagnosticEntries
		GROUP BY	SUBSTRING(Binary, @CurrentBitPosition, 1)
		ORDER BY	COUNT(*) ASC,
					Bit ASC
	)
	SET @CurrentBitPosition = @CurrentBitPosition + 1
END

DECLARE @OxygenGeneratorRating int =
(
	SELECT	SUM(CONVERT(int, SUBSTRING(Binary, Number, 1)) * POWER(2, @NumberOfBits - Number))
	FROM	dbo.DiagnosticReportEntry
			CROSS JOIN dbo.Numbers
	WHERE	Number <= LEN(Binary)
)

DECLARE @CO2ScrubberRating int =
(
	SELECT	SUM(CONVERT(int, SUBSTRING(Binary, Number, 1)) * POWER(2, @NumberOfBits - Number))
	FROM	#CO2ScrubberDiagnosticEntries
			CROSS JOIN dbo.Numbers
	WHERE	Number <= LEN(Binary)
)

SELECT
	@OxygenGeneratorRating [Oxygen Generator Ratng],
	@CO2ScrubberRating [CO2ScrubberRating],
	@OxygenGeneratorRating * @CO2ScrubberRating [Solution]
