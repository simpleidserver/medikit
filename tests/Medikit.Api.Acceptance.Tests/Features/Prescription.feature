Feature: Prescription

Scenario: Get metadata prescription
	When execute HTTP GET request 'http://localhost/prescriptions/metadata'
	And extract JSON from body
	Then HTTP status code equals to '200'
	Then JSON 'prescriptionTypes.children[0].0.translations[0].en'='[P0]'