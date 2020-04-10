<template>
    <div class="p-6 bg-gray-800 min-h-screen flex justify-center items-center">
        <div class="w-full max-w-md">
            <logo class="block mx-auto w-full max-w-xs fill-white" height="50" />
            <form class="mt-8 bg-white rounded-lg shadow-xl overflow-hidden" @submit.prevent="submit">
                <div class="px-10 py-12">
                    <h1 class="text-center font-bold text-3xl">Olá &#128512;</h1>
                    <div class="mx-auto mt-6 w-24 border-b-2" />
                    <flash-messages />
                    <text-input v-model="form.email" :errors="$page.with.errors ? $page.with.errors.email : []" class="mt-10" label="Email" type="email" autofocus autocapitalize="off" />
                    <text-input v-model="form.password" :errors="$page.with.errors ? $page.with.errors.password : []" class="mt-6" label="Password" type="password" />
                    <label class="mt-6 select-none flex items-center" for="remember">
                        <input id="remember" v-model="form.remember" class="mr-1" type="checkbox">
                        <span class="text-sm">Lembrar</span>
                    </label>
                </div>
                <div class="px-10 py-4 bg-gray-100 border-t border-gray-200 flex justify-between items-center">
                    <a class="hover:underline" tabindex="-1" href="#reset-password">Esqueceu a password?</a>
                    <loading-button :loading="sending" class="btn-gray" type="submit">Login</loading-button>
                </div>
            </form>
        </div>
    </div>
</template>

<script lang="ts">
    // @ts-ignore
    import FlashMessages from "@/Shared/FlashMessages.vue";
    // @ts-ignore
    import LoadingButton from '@/Shared/LoadingButton'
    // @ts-ignore
    import Logo from '@/Shared/Logo'
    // @ts-ignore
    import TextInput from '@/Shared/TextInput'
    import Vue from "vue";
    export default Vue.extend( {
        metaInfo: { title: 'Login' },
        components: {
            LoadingButton,
            Logo,
            FlashMessages,
            TextInput,
        },
        props: {
            errors: Object,
        },
        data() {
            return {
                sending: false,
                form: {
                    email: 'johndoe@example.com',
                    password: '',
                    remember: null,
                },
            }
        },
        methods: {
            submit() {
                this.sending = true;
                this.$inertia.post(`Login?returnUrl=${this.$page.controller.returnUrl}`, {
                    email: this.form.email,
                    password: this.form.password,
                    remember: this.form.remember,
                }).then(() => this.sending = false)
            },
        },
    });
</script>