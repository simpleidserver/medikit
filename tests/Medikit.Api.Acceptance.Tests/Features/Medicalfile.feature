Feature: Medicalfile

Scenario: Add medicalfile
	When execute HTTP POST JSON request 'http://localhost/medicalfiles'
	| Key        | Value |
	| patient_id | 1     |

	And extract JSON from body
	And extract 'id' from JSON body into 'id'
	And execute HTTP GET request 'http://localhost/medicalfiles/$id$'
	And extract JSON from body
	Then HTTP status code equals to '200'
	Then JSON 'firstname'='thierry'
	Then JSON 'lastname'='habart'
	Then JSON 'niss'='071089'
		
Scenario: Get metadata prescription
	When execute HTTP GET request 'http://localhost/medicalfiles/id/prescriptions/metadata'
	And extract JSON from body
	Then HTTP status code equals to '200'
	Then JSON 'prescriptionTypes.children[0].0.translations[0].en'='[P0]'