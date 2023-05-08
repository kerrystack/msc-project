import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
	scenarios: {
		low_mode_start: {
			executor: 'constant-vus',
			vus: 50,
			duration: '5m',
			gracefulStop: '0s',
		},
	},
}



export default function () {
	http.get('http://localhost:8001');
	sleep(1);
}