const readline = require('readline').createInterface({
	input: process.stdin,
	output: process.stdout
});

var readInput = function readInput(done){
	readline.question(`What's your name?`, (name) => {
		console.log(`Hi ${name}!`);
		done();
		readInput(done)
	});
};

let counter = 0;
readInput(() => {
	counter++;
	console.log("next..." + counter);
});