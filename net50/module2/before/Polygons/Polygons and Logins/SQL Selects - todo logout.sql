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

select * from polygon.sessionActivity
select * from polygon.sessionID

select * from polygon.activeSessionsByUser 