import axios from 'axios';
import { API_HOST_PREFIX, onGlobalSuccess } from './serviceHelpers';

const endpoint = `${API_HOST_PREFIX}/api/pollsters/grades`;

const getGradesByElectionId = (electionId) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/${electionId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess);
};

export { getGradesByElectionId };
