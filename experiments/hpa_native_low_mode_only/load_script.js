import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
	scenarios: {
		low_mode_only: {
			executor: 'constant-vus',
			vus: 1,
			duration: '10m',
			gracefulStop: '0s',
		},
	},
}



export default function () {
	http.get('http://localhost:8001');
	sleep(10);
}