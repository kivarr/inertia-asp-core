<template>
  <button @click="buttonClick" :disabled="loading || invalid" :class="['flex', 'items-center', invalid ? 'opacity-50 cursor-not-allowed' : '']">
    <div v-if="loading" class="btn-spinner mr-2" />
    <slot />
  </button>
</template>

<script>
export default {
  props: {
    invalid: {
      type: Boolean,
      default: false
    },
    loading: Boolean,
  },
  methods: {
    buttonClick() {
      this.$emit('buttonClick');
    }
  }
}
</script>
