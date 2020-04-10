<template>
    <layout>
    <div class="flex justify-center mt-12">
        <p>Use this page to detail your site's privacy policy.</p>

        <p @click="postTest">BUTTON TEST</p>
    </div>
    </layout>
</template>

<script lang="ts">
    import Vue from "vue";
    // @ts-ignore
    import Layout from "../../Shared/Layout.vue";
    export default Vue.extend({
        name: 'Privacy',
        components: {
            Layout
        },
        mounted(): void {
        },
        methods: {
            postTest() {
                this.$inertia.post("Test", "a", {
                    replace: false,
                    preserveState: true,
                    preserveScroll: false,
                    only: [],
                }) 
            }
        },
        data() {
            return {
                message: 'Hello Vue from TypeScript!',
            }
        }
    });
</script>