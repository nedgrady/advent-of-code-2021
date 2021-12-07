DROP TABLE IF EXISTS dbo.DiagnosticReportEntry
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

DECLARE
	@NumberOfBits int = 12,
	@CurrentBitPosition int = 1,
	@CurrentGammaRate int = 0,
	@CurrentEpsilonRate int = 0

WHILE @CurrentBitPosition <= @NumberOfBits
BEGIN
	DECLARE @CurrentBitValueToSet int =
	(
		SELECT Bit FROM 
		(
			SELECT	TOP (1) SUBSTRING(Binary, @CurrentBitPosition, 1) [Bit],
					COUNT(*) [Count Of Bit]
			FROM	dbo.DiagnosticReportEntry
			GROUP BY	SUBSTRING(Binary, @CurrentBitPosition, 1)
			ORDER BY [Count Of Bit] DESC
		) AS MostCommonBit
	)

	DECLARE @CurrentGammaBitMask int = POWER(2,@NumberOfBits - @CurrentBitPosition)
	
	IF @CurrentBitValueToSet = 1
	BEGIN
		SET @CurrentGammaRate = @CurrentGammaRate + @CurrentGammaBitMask
	END
	ELSE
	BEGIN
		SET @CurrentEpsilonRate = @CurrentEpsilonRate + @CurrentGammaBitMask
	END
	
	SET @CurrentBitPosition = @CurrentBitPosition + 1
END

SELECT
	@CurrentGammaRate [Gamma Rate],
	@CurrentEpsilonRate [Epsilon Rate],
	@CurrentGammaRate * @CurrentEpsilonRate [Solution]

