console.log('main.js loading...');
import { createApp } from 'vue'
import './style.css'
import DashboardPage from './pages/DashboardPage.vue';

const mounts = {
    'dashboard-page' : DashboardPage,
}

Object.entries(mounts).forEach(([id, component]) => {
    const element = document.getElementById(id);

    if (!element) return;

    const props = element.dataset.props
        ? JSON.parse(element.dataset.props)
        : {};

    createApp(component, props).mount(element);
})
console.log('main.js loaded!');
