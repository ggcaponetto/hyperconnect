const os = require('os');
const readline = require('readline').createInterface({
	input: process.stdin,
	output: process.stdout
});

let readInput = function readInput(done){
	readline.question(`Waiting for Unity to send a command.${os.EOL}`, (command) => {
		console.log(`Unty sent: ${command}`);
		done();
		readInput(done)
	});
};

let counter = 0;
readInput(() => {
	counter++;
	console.log(`(${counter}) waiting for next input...`);
});