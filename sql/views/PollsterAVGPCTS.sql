SELECT        PS.Name, 
              pr.PollsterId, 
              C.Id AS CandidateId, 
              COUNT(pr.SampleSize) AS TotalPolls, 
              AVG(pr.Percentage) AS AVGPCT
       
FROM          dbo.PollResults AS pr 
              INNER JOIN dbo.Pollsters AS PS 
              ON PS.Id = pr.PollsterId 
              INNER JOIN dbo.Candidates AS C 
              ON pr.CandidateId = C.Id

WHERE         (pr.Percentage IS NOT NULL)

GROUP BY      pr.PollsterId, 
              C.Id, 
              PS.Name
