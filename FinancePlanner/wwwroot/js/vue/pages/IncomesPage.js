export default {
    data() {
        return {
            message: "Hello from Vue!"
        }
    },
    mounted() {
        console.log("Vue test page mounted")
    },
    template: `<p>{{ message }}</p>`
}