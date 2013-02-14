IDNumberValidation
==================

.NET library for all sorts of identification number validations (person numbers, company numbers, ...).

Each country may have one or more mechanisms to uniquely identify their citizens and organisations (for instance social security number in the USA).
In most cases, these numbers are well structured and a validation logic can be written to determine if a given number is valid or not.

The purpose of this library is to provide a common interface for identification numbers validation logic, and then provide as much concrete implementations of validators as possible.

The ultimate goal is obviously to be able to validate any identification number from any country.

As of today, the following validations are implemented:

NATIONAL
	EUROPE
		Belgium
			- National Number of a person
			- BIS Number of a person (WIP)
			- Crossroad Bank of Enterprise (CBE) company numbers