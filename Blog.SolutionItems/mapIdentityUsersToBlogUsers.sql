﻿declare @username varchar(max)
declare @aspusers table 
(
	username varchar(max),
	identityId varchar(max)
)

insert into @aspusers
select username, id 
from [blog_identity].[dbo].[aspnetusers]

declare aspusers_cursor cursor for
select username from @aspusers

open aspusers_cursor
fetch next from aspusers_cursor into @username   

while @@FETCH_STATUS = 0   
begin
	if exists (select * from [blog].[dbo].[users] where UserName = @username)
	begin
		update [blog].[dbo].[users] 
		set IdentityId = (select identityId from @aspusers where username = @username)
		where UserName = @username
	end
	
	fetch next from aspusers_cursor into @username   
end   

close aspusers_cursor   
deallocate aspusers_cursor

select * from [blog].[dbo].[users]
select * from [blog_identity].[dbo].[aspnetusers]