import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
	scenarios: {
		low_mode_start: {
			executor: 'constant-vus',
			vus: 1,
			duration: '1m',
			gracefulStop: '0s',
		},
		high_mode: {
			executor: 'constant-vus',
			vus: 50,
			duration: '3m',
			gracefulStop: '0s',
			startTime: '1m',
		},
		low_mode_end: {
			executor: 'constant-vus',
			vus: 1,
			duration: '6m',
			gracefulStop: '0s',
			startTime: '4m',
		},
	},
}



export default function () {
	http.get('http://localhost:8001');
	sleep(1);
}