USE TheCompany
GO

SELECT FirstName + ' ' + LastName as FullName, YearSalary
FROM dbo.Employees
WHERE YearSalary BETWEEN 100000 AND 150000
ORDER BY YearSalary ASC