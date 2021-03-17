Select p.PlayerName, p.PlayerRace, c.ClassName, ps.Strength, ps.Dexterity, ps.Constitution, ps.Intelligence, ps.Wisdom, ps.Charisma 
FROM Player P 
JOIN PlayerStat ps ON ps.playerId = p.playerId
JOIN Class c ON p.ClassId = c.ClassId