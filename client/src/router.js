import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './views/App';
import Loader from "./views/Loader";
import Auth from "./views/Auth";
import Register from "./views/Register";
import Projects from "./views/Projects";
import Tasks from "./views/Tasks";
import People from "./views/People";
import AddProject from "./views/AddProject";
import EditTask from "./views/EditTask";
import Springs from "./views/Springs";
import EditProject from "./views/EditProject";
import AddPerson from "./views/AddPerson";
import AddSpring from "./views/AddSpring";
import EditSpring from "./views/EditSpring";
import AddTask from "./views/AddTask";
import Logs from "./views/Logs";
import Commits from "./views/Commits";
import Diagrams from "./views/Diagrams";
import Diagram from "./views/Diagram";

Vue.use(VueRouter);
export default new VueRouter({
	mode: 'history',
	routes: [
		{ path: "/", name: "loader", component: Loader },
		{ path: "/app", component: App, children: [
				{path: "auth", name: "auth", component: Auth },
        {path: "register", name: "register", component: Register},
        {path: "projects", name: "projects", component: Projects},
        {path: "projects/add", name:"addProject", component: AddProject},
        {path: "projects/edit", name: "editProject", component: EditProject},
        {path: "logs", name:"logs", component:Logs},
        {path: "people", name: "people", component: People},
        {path: "people/add", name:"addPeople", component: AddPerson},
        {path: "tasks", name:"tasks", component: Tasks},
        {path: "tasks/editTask", name:"editTask", component: EditTask},
        {path: "tasks/addTask", name:"addTask", component: AddTask},
        {path: "springs", name: "springs", component: Springs},
        {path: "springs/add", name:"addSpring", component: AddSpring},
        {path: "springs/edit", name:"editSpring", component: EditSpring},
        {path: "github", name:"github", component: Commits},
        {path: "diagrams", name:"diagrams", component: Diagrams},
        {path: "diagrams/complete", name: "diagram", component: Diagram}
		]}
	]
});
