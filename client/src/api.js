import { apiUrl } from "./config";
import axios from "axios";
import {gitUrl} from "./config";

export default {
	user: {
		resourceUrl: "user",
		getAll() {
			return axios({
				url: `${apiUrl}/${this.resourceUrl}`,
				method: "GET"
			});
		},
		getOne(id) {
			return axios({
				url: `${apiUrl}/${this.resourceUrl}/${id}`,
				method: "GET"
			});
		},
		add(item) {
			return axios({
				url: `${apiUrl}/${this.resourceUrl}`,
				method: "POST",
				data: item
			});
		},
		edit(id, item) {
			return axios({
				url: `${apiUrl}/${this.resourceUrl}/${id}`,
				method: "PUT",
				data: item
			});
		},
		delete(id) {
			return axios({
				url: `${apiUrl}/${this.resourceUrl}/${id}`,
				method: "DELETE"
			});
		},
	},
  category: {
    resourceUrl: "project/category",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(category) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: category
      });
    },
    edit(id, category) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: category
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  logs: {
    resourceUrl: "project/log",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(logs) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: logs
      });
    },
    edit(id, logs) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: logs
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  people: {
    resourceUrl: "project/people",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(people) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: people
      });
    },
    edit(id, people) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: people
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  project: {
    resourceUrl: "project",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(project) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: project
      });
    },
    edit(id, project) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: project
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  repository: {
    resourceUrl: "repository",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(repository) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: repository
      });
    },
    edit(id, repository) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: repository
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  roles: {
    resourceUrl: "user/role",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(role) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: role
      });
    },
    edit(id, role) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: role
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  sLogs: {
    resourceUrl: "project/slogs",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(slogs) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: slogs
      });
    },
    edit(id, slogs) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: slogs
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  spring: {
    resourceUrl: "project/spring",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(spring) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: spring
      });
    },
    edit(id, spring) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: spring
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  tasks: {
    resourceUrl: "project/task",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(task) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: task
      });
    },
    edit(id, task) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: task
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  tracker: {
    resourceUrl: "project/tracker",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(tracker) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: tracker
      });
    },
    edit(id, tracker) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: tracker
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  wiki: {
    resourceUrl: "project/wiki",
    getAll() {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "GET"
      });
    },
    getOne(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "GET"
      });
    },
    add(wiki) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}`,
        method: "POST",
        data: wiki
      });
    },
    edit(id, wiki) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "PUT",
        data: wiki
      });
    },
    delete(id) {
      return axios({
        url: `${apiUrl}/${this.resourceUrl}/${id}`,
        method: "DELETE"
      });
    }
  },
  gitHub:{
	  getAll(repoName){
	    return axios({
        url: `${gitUrl}`+`${repoName}/commits`,
        method: "GET"
      })
    }
  }
};
