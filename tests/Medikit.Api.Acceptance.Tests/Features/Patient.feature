Feature: Patient

Scenario: Get patient by NISS
	When execute HTTP GET request 'http://localhost/patients/071089'
	And extract JSON from body
	Then HTTP status code equals to '200'
	Then JSON 'firstname'='thierry'