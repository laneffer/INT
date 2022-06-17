GO
/****** Object:  StoredProcedure [dbo].[PollstersPctDiff_SelectByElectionId]    Script Date: 6/16/2022 10:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Alisha Lane
-- Create date: 5/27/2022
-- Description: This proc gets a list of pollster percent differences based on total polls from their average by election id
-- Code Reviewer: Ali Azimi

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note:
-- =============================================
ALTER proc 	 [dbo].[PollstersPctDiff_SelectByElectionId]
		 @ElectionId int
as

/*
Declare		 @ElectionId int = 1

Execute		 [dbo].[PollstersPctDiff_SelectByElectionId]
	         @ElectionId				
*/

BEGIN

Select		 PV.PollsterId,
	         PV.Name AS Pollster,
		 PV.CandidateId,
	         PV.TotalPolls,
	         cast(ROUND(PV.AVGPCT,2) as numeric(38,2)) AS AVGPCT,
	         cast(ROUND(PV.AVGPCT - ER.[Percentage],2) as numeric(36,2)) AS Diff

From	         [dbo].[PollsterAVGPCTS] AS PV
		 JOIN  dbo.ElectionResults as ER 
		 ON ER.CandidateId = PV.CandidateId 
		 AND ElectionId = @ElectionId
				
END
			
