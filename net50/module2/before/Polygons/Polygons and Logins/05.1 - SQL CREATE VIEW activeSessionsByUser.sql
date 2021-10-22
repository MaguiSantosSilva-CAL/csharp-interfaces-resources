USE [maguiss]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [polygon].[activeSessionsByUser] AS

with ids as (
select max(psi.id) as id
from polygon.sessionID psi 
right join polygon.sessionActivity psa on psa.sessionID = psi.sessionID
group by psi.username
)

select	 pu.username
		,pu.dateLastLogin [last login attempt]
		,psa.created as [sessionID created]
		,pu.userLoggedIn
		,psa.lastAccountActivityDuringSession
		,case 
		 when DATEDIFF(MINUTE, psa.lastAccountActivityDuringSession, CURRENT_TIMESTAMP) > 30 then 0
		 else 1
		 end as activeInLast30Mins 
		,DATEDIFF(minute,lastAccountActivityDuringSession,CURRENT_TIMESTAMP) as minsSinceLastActivity
		,case 
		 when DATEDIFF(MINUTE, psa.lastAccountActivityDuringSession, CURRENT_TIMESTAMP) > 30 then null
		 else psa.sessionID
		 end as activeSession

from polygon.sessionActivity psa
join polygon.sessionID		 psi on psa.sessionID = psi.sessionID
join polygon.users			 pu	 on pu.username	  = psi.username
where 1=1 
and psi.id in (select id from ids)


GO

use maguiss
create synonym polygon.activeSessionsByUser for polygon.usp_activeSessionsByUser
GO


