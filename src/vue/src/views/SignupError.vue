<template>
    <div class="content-wrapper">
        <div class="content">
            <h2>
                Sorry but we've failed to sign you up
            </h2>
            <p>
                If you have an appetite for more good deeds? <br />
                See other available events.
            </p>
            <router-link to="/events" class="power_button effect_1">
                <span class="button_value">Go to Events!</span>
            </router-link>
        </div>
    </div>
</template>
<script>
export default {};
</script>
<style lang="sass" scoped>
.first-name
  text-transform: capitalize

.content-wrapper
  max-width: 800px
  margin: 36px auto
  border-radius: 0px
  padding: 60px
  box-shadow: 0px 0px 13px rgba(253, 27, 91, 0.1)
  .content
    h2, p
      padding-bottom: 36px
    p
      max-width: 450px
      margin: 0 auto
</style>
