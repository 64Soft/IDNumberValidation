IDNumberValidation
==================

.NET library for all sorts of identification number validations (person numbers, company numbers, ...).

Each country may have one or more mechanisms to uniquely identify their citizens and organisations (e.g. http://en.wikipedia.org/wiki/National_identification_number ).
In most cases, these numbers are well structured and a validation logic can be written to determine if a given number is valid or not.

The purpose of this library is to provide a common interface for identification numbers validation logic, and then provide as much concrete implementations of validators as possible.

The ultimate goal is obviously to be able to validate any identification number from any country.


Following validations are implemented:

# NATIONAL
## EUROPE
### BELGIUM
* NR Number (person, National Registry number)
* BIS Number (person, Social Security number for foreign people living or working in Belgium)
* CBE Number (company, Crossroad Bank of Enterprises)
