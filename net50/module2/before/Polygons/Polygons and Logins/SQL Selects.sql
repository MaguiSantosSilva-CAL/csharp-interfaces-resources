USE maguiss
go
						   
select * from polygon.users	
select * from polygon.sessionID
select * from polygon.sessionActivity
select * from polygon.activeSessionsByUser

-- Session is Idle
select username from polygon.activeSessionsByUser
where activeSession is not null
and activeInLast30Mins = 0


-- Log user out
BEGIN TRANSACTION

DECLARE @idleSessions table (idleSessions uniqueidentifier)
DECLARE @usersWithIdleSessions


update polygon.users
set userLoggedin = 0,
sessionID = null
where username in (select username from polygon.activeSessionsByUser
where activeSession is not null
and activeInLast30Mins = 0)


COMMIT

DECLARE @username varchar(50) = 'Magui'




IF polygon.fn_getActiveSessionID(@username) is null
	update polygon.users 
	set userLoggedin = 0
	where username	 = @username

select polygon.fn_getActiveSessionID('NewUser')
select polygon.fn_getActiveSessionID('Admin')




--select psi.id, psi.username, psa.*
--from polygon.sessionID psi
--right join polygon.sessionActivity psa on psi.sessionID = psa.sessionID
--order by username, id